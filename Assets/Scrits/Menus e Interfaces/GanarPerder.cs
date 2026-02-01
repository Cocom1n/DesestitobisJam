using UnityEngine;
using UnityEngine.SceneManagement;

public class GanarPerder : MonoBehaviour
{
    //  TODABIA NO HAY CONDICION DE VICTORIA
   private string nombreEscenaJuego;
    //[SerializeField] private GameObject panelGanar;
    [SerializeField] private GameObject panelPerder;

    [SerializeField] private SerElectrocutado ScriptJugador;

    [SerializeField] private GameObject[] objetosADesactivar;

      [Header("Audio")]
    [SerializeField] private AudioSource musicaFondo;     
    [SerializeField] private AudioSource sonidoDerrota;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //panelGanar.SetActive(false); 
        panelPerder.SetActive(false);

        ScriptJugador.AlMorir += MostrarPanelPerder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MostrarPanelPerder()
    {
            Time.timeScale = 0f;
        foreach (GameObject objeto in objetosADesactivar)
        {
            if (objeto != null)
            {
                objeto.SetActive(false);
            }
            if (musicaFondo != null && musicaFondo.isPlaying)
            musicaFondo.Stop();
               if (sonidoDerrota != null)
            sonidoDerrota.Play();

        }

        panelPerder.SetActive(true);
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
