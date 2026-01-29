using UnityEngine;
using System.Collections;

/** Mascara recolectable que aplica todos sus efectos al jugador */
public class MaskCollectable : CollectibleItem
{
    /** Datos de la mascara */
    [SerializeField] private MaskData maskData;

    /** Indica si la mascara ya expiro para evitar re-recoleccion */
    private bool expiro;


    // Nueva propiedad pública de solo lectura
    public bool Expiro => expiro;

    public MaskData MaskData => maskData;

    /** Referencia para el efecto de titileo (Solo para temporales) */
    private Renderer targetRenderer;
    private MaterialPropertyBlock propBlock;
    private static readonly int FlickerIntensityID = Shader.PropertyToID("_FlickerIntensity");

    /** Se llama cuando la mascara deja de estar equipada */
    public System.Action<MaskCollectable> OnMaskReleased;


    private void Start()
    {
        // Solo inicializamos el bloque si la mascara es temporal
        if (maskData != null && maskData.lifetime > 0)
        {
            targetRenderer = GetComponent<Renderer>();
            propBlock = new MaterialPropertyBlock();
        }
    }

    /** Aplica los efectos de la mascara al recolectar */
    public void RecolectarMask(EffectController effects, Transform puntoCabeza)
    {
        if (maskData == null || effects == null || expiro) return;
        base.Recolectar(puntoCabeza);
        effects.ApplyMask(maskData, this);

        // Resetear intensidad si es temporal
        if (maskData.lifetime > 0)
        {
            UpdateFlicker(0);
        }

        Debug.Log($"Mascara {maskData.maskName} recolectada");
    }

    /** Logica de desactivacion cuando se agota el tiempo (Solo para temporales) */
    public void Expirar()
    {
        // Seguridad: solo expira si es temporal y no ha expirado
        if (maskData == null || maskData.lifetime <= 0 || expiro) return;
        
        expiro = true;

        // Desvincular del jugador
        base.Soltar();
        OnMaskReleased?.Invoke(this);

        Debug.Log($"Mascara {maskData.maskName} expiro. Iniciando titileo");
        
        StartCoroutine(EsperaYDesactiva(2.0f));
    }

    /** Prepara la mascara para ser reutilizada */
    public void Reactivar()
    {
        expiro = false;
        
        if (maskData != null && maskData.lifetime > 0)
        {
            UpdateFlicker(0);
        }
        
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = true;

        if (targetRenderer != null) targetRenderer.enabled = true;

        gameObject.SetActive(true);
    }

    /** Corrutina para manejar el titileo y la desactivacion (Solo temporales) */
    private IEnumerator EsperaYDesactiva(float tiempo)
    {
        float transcurrido = 0;
        
        while (transcurrido < tiempo)
        {
            transcurrido += Time.deltaTime;
            float progreso = transcurrido / tiempo;
            
            // Efecto visual solo si tenemos los componentes
            UpdateFlicker(progreso);

            yield return null;
        }
        
        UpdateFlicker(0);
        if (targetRenderer != null) targetRenderer.enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        gameObject.SetActive(false);
    }

    /** Actualiza la intensidad del titileo */
    private void UpdateFlicker(float value)
    {
        if (targetRenderer == null || propBlock == null) return;
        
        targetRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat(FlickerIntensityID, value);
        targetRenderer.SetPropertyBlock(propBlock);
    }
}
