using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/** Clase que maneja el temporizador de los efectos */
public class EffectTimer : MonoBehaviour
{
    [Header("Elementos UI")]
    public Slider timerSlider;               // Slider para mostrar el progreso de tiempo restante
    private float effectTimeRemaining;       // Tiempo restante del efecto
    private bool effectActive = false;
    private MaskData maskData;

    private Image backgroundImage;           // Imagen del fondo del slider

    /** Metodo para comenzar el temporizador de un efecto */
    public void StartEffectTimer(MaskData maskData)
    {
        this.maskData = maskData;
        effectTimeRemaining = maskData.lifetime;
        timerSlider.gameObject.SetActive(true);   // Activamos el slider cuando comienza el efecto
        timerSlider.value = 0f;  // Inicializamos el slider en cero

        // Obtener el backgroundImage solo una vez
        backgroundImage = timerSlider.transform.GetChild(0).GetComponent<Image>();
        backgroundImage.sprite = maskData.icon;

        effectActive = true;

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

            // Actualizamos el slider y el fillAmount
            timerSlider.value = 1 - (effectTimeRemaining / maskData.lifetime);
            backgroundImage.fillAmount = timerSlider.value;

            // Esperamos un frame antes de seguir
            yield return null;
        }

        // Cuando el tiempo termine, desactivamos el temporizador
        EndEffect();
    }

    /** Termina el efecto y oculta la barra */
    private void EndEffect()
    {
        effectActive = false;
        timerSlider.gameObject.SetActive(false);  // Desactivamos el slider cuando el efecto haya terminado
    }
}
