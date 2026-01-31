using UnityEngine;
using TMPro;

/** Clase que maneja las animaciones de la interfaz de usuario */
public class AnimacionesUI : MonoBehaviour
{
    public GameObject powerUI;  // Referencia al UI del PowerUp

    /** Animacion de Fade para el texto */
    public float AnimarTextoFade(TextMeshProUGUI texto)
    {
        texto.alpha = 0;

        LeanTween.value(gameObject, 0f, 1f, 0.6f)
            .setOnUpdate(v => texto.alpha = v);

        return 0.6f;
    }

    /** Animacion de deslizamiento para el nombre del texto */
    public float AnimarTextoNombre(TextMeshProUGUI texto)
    {
        RectTransform rt = texto.rectTransform;
        rt.anchoredPosition = new Vector2(1200f, rt.anchoredPosition.y);

        // Movimiento hacia el centro con duracion ajustada
        float moveDuration = 1f;
        LeanTween.moveX(rt, 0f, moveDuration)
            .setFrom(1200f)
            .setEaseOutBack();

        // Movimiento hacia la izquierda con tiempo de espera
        float delayTime = 2.5f;
        LeanTween.moveX(rt, -1200f, moveDuration)
            .setDelay(delayTime)
            .setEaseInOutBack();

        return moveDuration + delayTime + moveDuration; 
    }

    /** Animacion para la entrada del PowerUp */
    public float AnimarPowerUIEntrada()
    {
        if (powerUI == null) return 0f;

        float duration = 0.8f;

        powerUI.transform.localScale = Vector3.one * 0.3f;

        LeanTween.moveLocalX(powerUI, powerUI.transform.localPosition.x, duration)
            .setFrom(powerUI.transform.localPosition.x - 300f)
            .setEaseOutCubic();

        LeanTween.scale(powerUI, Vector3.one, duration)
            .setEaseOutBack();

        return duration;
    }

    /** Animacion para la salida del PowerUp */
    public float AnimarPowerUISalida()
    {
        if (powerUI == null) return 0f;

        float duration = 0.5f;
        LeanTween.scale(powerUI, Vector3.zero, duration)
            .setEaseInBack();

        return duration;
    }

    /** Animacion tipo pop para el texto */
    public float AnimarTexto2(TextMeshProUGUI texto)
    {
        texto.rectTransform.localScale = Vector3.zero;
        LeanTween.scale(texto.rectTransform, Vector3.one, 0.5f)
            .setEaseOutBack()
            .setDelay(0.3f);

        return 0.3f + 0.5f;
    }

    /** Animacion para que el texto aparezca desde la derecha y luego desaparezca */
    public float AnimarFade(TextMeshProUGUI texto)
    {
        texto.alpha = 0;
        RectTransform rt = texto.rectTransform;
        rt.anchoredPosition = new Vector2(120f, rt.anchoredPosition.y);

        float delay = 0.1f;

        // Entrada desde la derecha
        LeanTween.moveX(rt, 0f, 0.5f).setEaseOutCubic().setDelay(0.6f + delay);
        LeanTween.value(gameObject, 0f, 1f, 0.5f).setDelay(0.6f + delay).setOnUpdate(v => texto.alpha = v);

        // Salida hacia la izquierda
        LeanTween.moveX(rt, -120f, 0.5f).setEaseInCubic().setDelay(1.6f + delay);
        LeanTween.value(gameObject, 1f, 0f, 0.5f).setDelay(1.6f + delay).setOnUpdate(v => texto.alpha = v);

        return 2.6f + delay;
    }
}