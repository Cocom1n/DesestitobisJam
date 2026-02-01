using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CondicionVictoria : MonoBehaviour
{
    [SerializeField] private float tiempoLimite = 60f;

    [SerializeField] private GameObject panelVictoria;
    [SerializeField] private GameObject panelDerrota;

    [SerializeField] private TextMeshProUGUI textoContador;
    [SerializeField] private TextMeshProUGUI textoHielosVictoria;
    [SerializeField] private SerElectrocutado jugador;

    [SerializeField] private GameObject[] objetosADesactivar;
    [SerializeField] private AudioSource musicaVictoria;
    [SerializeField] private AudioSource musicaFondo;



    private float tiempoRestante;
    private bool contadorActivo = false;
    private bool juegoTerminado = false;

    void Start()
    {
        tiempoRestante = tiempoLimite;

        if (panelVictoria != null)
            panelVictoria.SetActive(false);
        if (panelDerrota != null)
            panelDerrota.SetActive(false);
    }

    void Update()
    {
        if (contadorActivo && !juegoTerminado)
        {
            tiempoRestante -= Time.deltaTime;

            if (textoContador != null)
            {
                int minutos = Mathf.FloorToInt(tiempoRestante / 60);
                int segundos = Mathf.FloorToInt(tiempoRestante % 60);
                textoContador.text = string.Format("{0:00}:{1:00}", minutos, segundos);
            }

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                MostrarDerrota();
            }
        }
    }

    public void IniciarContador()
    {
        if (!juegoTerminado && !contadorActivo)
        {
            contadorActivo = true;
        }
    }

    public void MostrarVictoria()
    {
        Time.timeScale = 0f;
        
        if (juegoTerminado) return;

        juegoTerminado = true;
        contadorActivo = false;

        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }

        if (textoHielosVictoria != null && jugador != null)
        {
            textoHielosVictoria.text = "Hielos: " + jugador.ObtenerVidaActual();
        }

        foreach (GameObject objeto in objetosADesactivar)
        {
            if (objeto != null)
            {
                objeto.SetActive(false);
            }
        }
        if (musicaFondo != null && musicaFondo.isPlaying)
        {
            musicaFondo.Stop();
        }
        if (musicaVictoria != null)
         {
            musicaVictoria.Play();
         }

        
    }

    public void MostrarDerrota()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        contadorActivo = false;

        if (panelDerrota != null)
        {
            panelDerrota.SetActive(true);
        }
        
        foreach (GameObject objeto in objetosADesactivar)
        {
            if (objeto != null)
            {
                objeto.SetActive(false);
            }
        }
         

        // Time.timeScale = 0f;

    }

    public void Reiniciar()
    {
        juegoTerminado = false;
        contadorActivo = false;
        tiempoRestante = tiempoLimite;
        Time.timeScale = 1f;

        if (panelVictoria != null)
            panelVictoria.SetActive(false);
        if (panelDerrota != null)
            panelDerrota.SetActive(false);
    }
}
