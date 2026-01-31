using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// NO TOCAR EL ****** backgroundImage ******

/** Clase que maneja el temporizador de los efectos */
public class EffectTimer : MonoBehaviour
{
    [Header("Elementos UI")]
    public Image imgCenter;
    public Slider timerSlider;
    private float effectTimeRemaining;
    private MaskData maskData;

    //private Image bachground;

    /** Metodo para comenzar el temporizador de un efecto */
    public void StartEffectTimer(MaskData maskData)
    {
        this.maskData = maskData;
        effectTimeRemaining = maskData.lifetime;
        timerSlider.gameObject.SetActive(true);   // Activamos el slider
        timerSlider.value = 0f;

        //bachground = timerSlider.transform.GetChild(0).GetComponent<Image>();
        //bachground.sprite = maskData.icon;
        imgCenter.sprite = maskData.icon;

        // Iniciar la corutina para actualizar el temporizador
        StartCoroutine(TimerCoroutine());
    }

    /** Corutina que maneja el temporizador */
    private IEnumerator TimerCoroutine()
    {
        // Mientras el tiempo restante sea mayor a cero
        while (effectTimeRemaining > 0)
        {
            effectTimeRemaining -= Time.deltaTime;

            timerSlider.value = 1 - (effectTimeRemaining / maskData.lifetime);

            yield return null;
        }
        yield return new WaitForSeconds(0.8f);
        timerSlider.gameObject.SetActive(false);  // Desactivamos el slider cuando el efecto haya terminado
    }

}
