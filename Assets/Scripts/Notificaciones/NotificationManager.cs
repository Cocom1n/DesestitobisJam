using UnityEngine;
using TMPro;
using System.Collections;
using System.Linq;

public class NotificationManager : MonoBehaviour
{
    [Header("Elementos UI")]
    public TextMeshProUGUI maskNameText;    // Para mostrar el nombre de la mask
    public TextMeshProUGUI effectsText;     // Para mostrar los efectos de la mask
    public TextMeshProUGUI descriptionText; // Para mostrar la descripción de la mask

    private Coroutine notificationCoroutine;
    public EffectTimer effectTimer;

    // Implementación de patrón singleton para fácil acceso
    public static NotificationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, destruimos la nueva 
        }

    }

    public void ShowMaskInfo(MaskData maskData)
    {
        if (notificationCoroutine != null)
        {
            StopCoroutine(notificationCoroutine);
        }

        // Iniciar una nueva corutina para mostrar la notificacion
        notificationCoroutine = StartCoroutine(DisplayNotification(maskData));
    }

    private IEnumerator DisplayNotification(MaskData maskData)
    {
        // Mostrar nombre y descripción
        maskNameText.text = maskData.maskName;
        descriptionText.text = "Descripcion: " + (maskData.name ?? "No disponible");

        // Mostrar los efectos
        effectsText.text = "Efectos:";
        foreach (var effectData in maskData.effects)
        {
            effectsText.text += $"\n- {effectData.effect.name} ( : {effectData.value})";
        }


        // Hacer visible la UI de la notificación
        maskNameText.gameObject.SetActive(true);
        effectsText.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);

        effectTimer.StartEffectTimer(maskData);


        // Esperar un tiempo antes de ocultar la notificación
        yield return new WaitForSeconds(3f); // Duración de la notificación en pantalla

        // Ocultar la UI después de la espera
        maskNameText.gameObject.SetActive(false);
        effectsText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
    }
}
