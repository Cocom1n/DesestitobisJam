using UnityEngine;
using TMPro;
using System.Collections;

/** Clase que maneja la visualizacion de la informacion de la mascara y coordina las animaciones */
public class NotificationManager : MonoBehaviour
{
    [Header("Elementos UI")]
    public TextMeshProUGUI maskNameText;      // Texto que muestra el nombre de la mascara
    public TextMeshProUGUI effectsText;       // Texto que muestra los efectos de la mascara
    public EffectTimer effectTimer;           // Temporizador de efectos

    [Header("Animadores")]
    public AnimacionesUI animator;            // Animaciones de UI (texto y PowerUp)

    private Coroutine notificationCoroutine;   // Corutina para mostrar la notificacion
    private Coroutine powerUpCoroutine;        // Corutina para animar el PowerUp

    public static NotificationManager Instance { get; private set; }  

    /** Metodo que inicializa la instancia de NotificationManager */
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /** Metodo que muestra la informacion de la mascara y coordina las animaciones de la notificacion y PowerUp. */
    public void ShowMaskInfo(MaskData maskData)
    {
        // Detenemos cualquier animacion en curso
        if (notificationCoroutine != null)
        {
            StopCoroutine(notificationCoroutine);
            notificationCoroutine = null;
        }

        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
            powerUpCoroutine = null;
        }

        notificationCoroutine = StartCoroutine(DisplayNotification(maskData));
        powerUpCoroutine = StartCoroutine(DisplayPowerUp(maskData));
    }

    /** Corutina que maneja la visualizacion de la notificacion. Muestra el nombre de la mascara y los efectos con animaciones. */
    private IEnumerator DisplayNotification(MaskData maskData)
    {
        maskNameText.text = maskData.maskName;  
        effectsText.text = "Efectos:";          

        if (maskData.effects != null)
        {
            foreach (var e in maskData.effects)
                effectsText.text += $"\n- {e.effect.name} : {e.value}"; // Mostramos los efectos de la mascara
        }

        // Animar el texto (nombre y efectos)
        float waitTime = Mathf.Max(
            animator.AnimarTextoNombre(maskNameText),
            animator.AnimarTextoNombre(effectsText)
        );
        // Iniciar el temporizador de efectos visuales
        effectTimer?.StartEffectTimer(maskData);

        yield return new WaitForSeconds(waitTime);
        yield return new WaitForSeconds(maskData.lifetime - waitTime);

        notificationCoroutine = null;  // Reseteamos la corutina de notificacion
    }

    /** Corutina que maneja la visualizacion del PowerUp. Muestra la animacion de entrada y salida del PowerUp. */
    private IEnumerator DisplayPowerUp(MaskData maskData)
    {
        animator?.AnimarPowerUIEntrada();
        yield return new WaitForSeconds(maskData.lifetime);
        animator?.AnimarPowerUISalida();

        powerUpCoroutine = null;  // Reseteamos la corutina del PowerUp
    }
}
