using UnityEngine;
using System.Collections;

/** Mascara que busca el socket 'Cabeza' por defect */
public class MaskCollectable : CollectibleItem
{
    [Header("Datos de Mascara")]
    [SerializeField] private MaskData maskData;

    /** Indica si la mascara ya expiro */
    private bool expiro;

    /** Referencia para el efecto de titileo */
    private Renderer targetRenderer;
    private MaterialPropertyBlock propBlock;
    private static readonly int FlickerIntensityID = Shader.PropertyToID("_FlickerIntensity");

    /** Se llama cuando la mascara deja de estar equipada */
    public System.Action<MaskCollectable> OnMaskReleased;

    protected override void Awake()
    {
        base.Awake();
        /** Configuracion por defecto para mascaras */
        nombreSocketObjetivo = "Cabeza";
        
        targetRenderer = GetComponent<Renderer>();
        if (maskData != null && maskData.lifetime > 0)
        {
            //targetRenderer = GetComponent<Renderer>();
            propBlock = new MaterialPropertyBlock();
        }
    }

    /** Solicita el anclaje y aplica sus efectos de la mascara */
    public override bool SerRecogido(IReceptorInteraccion receptor)
    {
        if (Expiro)
            return false;

        if (base.SerRecogido(receptor))
        {
            AplicarEfectos(receptor.ObtenerEfectos());
        Debug.Log($"Mascara {maskData.maskName} recolectada");
            return true;
        }
        return false;
    }

    private void AplicarEfectos(EffectController effects)
    {
        if (maskData != null && effects != null)
        {
            effects.ApplyMask(maskData, this);
            if (maskData.lifetime > 0) UpdateFlicker(0);
        }
    }
    /** Logica de desactivacion cuando se agota el tiempo */
    public void Expirar()
    {
        // verioficar expira si es temporal y no ha expirado
        if (maskData == null || maskData.lifetime <= 0 || expiro) return;
        
        expiro = true;
        base.Soltar();
        OnMaskReleased?.Invoke(this);
        Debug.Log($"Mascara {maskData.maskName} expiro, tittilar ...");

        StartCoroutine(EsperaYDesactiva(2.0f));
    }

    public override void Soltar()
    {
        base.Soltar();
        OnMaskReleased?.Invoke(this);
    }

    public void Reactivar()
    {
        expiro = false;
        if (maskData != null && maskData.lifetime > 0) UpdateFlicker(0);
        if (col != null) col.enabled = true;
        if (targetRenderer != null) targetRenderer.enabled = true;
        gameObject.SetActive(true);
    }

    private IEnumerator EsperaYDesactiva(float tiempo)
    {
        float transcurrido = 0;
        while (transcurrido < tiempo)
        {
            transcurrido += Time.deltaTime;
            UpdateFlicker(transcurrido / tiempo);
            yield return null;
        }
        UpdateFlicker(0);
        if (targetRenderer != null) targetRenderer.enabled = false;
        if (col != null) col.enabled = false;
        gameObject.SetActive(false);
    }

    private void UpdateFlicker(float value)
    {
        if (targetRenderer == null || propBlock == null) return;
        targetRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat(FlickerIntensityID, value);
        targetRenderer.SetPropertyBlock(propBlock);
    }

    /** Getters **/
    public MaskData MaskData => maskData;
    public bool Expiro => expiro;
}
