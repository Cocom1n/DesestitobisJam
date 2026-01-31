using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DisparoBombuchas : MonoBehaviour
{
    //[SerializeField] private GameObject bombucha;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private Camera camaraPrincipal;

    [SerializeField] private float fuerzaLanzamiento = 20f;
    [SerializeField] private float arcoVision = 180f;

    [SerializeField] private Bombucha[] poolDeBombuchas;
    [SerializeField] private Renderer rendererFlecha;
    private Color[] coloresIntensos = {
        Color.green,
        Color.yellow,
        Color.red,    
        Color.blue   
    };

    [SerializeField] private int maxMunicion = 3;
    private int municionActual;
    private bool estaRecargando = false;
    [SerializeField] private float tiempoRecarga = 10f;
    public bool PuedeDisparar => !estaRecargando && municionActual > 0;

    //cositas para la ui
    [SerializeField] private Image imagenBombucha;
    [SerializeField] private TextMeshProUGUI textoMunicion;
    private Color colorNormal = Color.white;
    private Color colorRecargando = new Color(1f, 1f, 1f, 0.3f);

    void Start()
    {
        municionActual = maxMunicion;
        ActualizarUI();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !estaRecargando)
        {
            LanzarHaciaMouse();
        }
    }

    private void LanzarHaciaMouse()
    {
        Ray ray = camaraPrincipal.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 direccionHaciaMouse = hit.point - transform.position;
            direccionHaciaMouse.y = 0;

            float angulo = Vector3.SignedAngle(transform.forward, direccionHaciaMouse, Vector3.up);

            float limite = arcoVision / 2f;
            angulo = Mathf.Clamp(angulo, -limite, limite);

            Vector3 direccionFinal = Quaternion.Euler(0, angulo, 0) * transform.forward;

            /*GameObject bala = Instantiate(bombucha, puntoDisparo.position, Quaternion.identity);
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direccionFinal * fuerzaLanzamiento, ForceMode.Impulse);
            }*/
            CambiarColorFlecha();

            foreach (Bombucha b in poolDeBombuchas)
            {
                if (!b.gameObject.activeInHierarchy)
                {
                    b.Lanzar(puntoDisparo.position, direccionFinal * fuerzaLanzamiento);

                    municionActual--;
                    ActualizarUI();

                    if (municionActual <= 0)
                    {
                        StartCoroutine(Recargar());
                    }

                    break; 
                }
            }

            //Debug.DrawRay(puntoDisparo.position, direccionFinal * 5, Color.red, 2f);

        }
    }

    private void CambiarColorFlecha()
    {
        if (rendererFlecha == null) return;

        int indiceAleatorio = Random.Range(0, coloresIntensos.Length);
        Color colorElegido = coloresIntensos[indiceAleatorio];

        rendererFlecha.material.color = colorElegido;
    }

    IEnumerator Recargar()
    {
        estaRecargando = true;
        
        if (imagenBombucha != null)
        {
            //imagenBombucha.color = colorRecargando;
            imagenBombucha.fillAmount = 0f;
        }
        
        if (textoMunicion != null)
        {
            textoMunicion.text = "0";
        }

        float tiempoTranscurrido = 0f;
        
        while (tiempoTranscurrido < tiempoRecarga)
        {
            tiempoTranscurrido += Time.deltaTime;
            float progreso = tiempoTranscurrido / tiempoRecarga;
            
            if (imagenBombucha != null)
            {
                imagenBombucha.fillAmount = progreso;
            }
            
            yield return null;
        }

        municionActual = maxMunicion;
        estaRecargando = false;
        
        if (imagenBombucha != null)
        {
            imagenBombucha.fillAmount = 1f;
            imagenBombucha.color = colorNormal;
        }
        
        ActualizarUI();
    }

    private void ActualizarUI()
    {
        if (textoMunicion != null)
        {
            textoMunicion.text = municionActual.ToString();
        }

        if (imagenBombucha != null && !estaRecargando)
        {
            imagenBombucha.fillAmount = 1f;
            imagenBombucha.color = colorNormal;
        }
    }

}
