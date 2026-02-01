using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string nombreEscenaJuego;
    [SerializeField] private GameObject panelTutorial;

    public void Jugar()
    {
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Tutorial()
    {
        panelTutorial.SetActive(true);
    }

    public void CerrarTutorial()
    {
        panelTutorial.SetActive(false);
    }
}
