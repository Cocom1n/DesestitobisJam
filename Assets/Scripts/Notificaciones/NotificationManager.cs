using UnityEngine;
using TMPro;
using System.Collections;

public class NotificationManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI maskNameText;
    public TextMeshProUGUI effectsText;
    public TextMeshProUGUI descriptionText;

    private Coroutine notificationCoroutine;

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

        // Iniciar una nueva corutina para mostrar la notificación
        notificationCoroutine = StartCoroutine(DisplayNotification(maskData));
    }

    private IEnumerator DisplayNotification(MaskData maskData)
    {
        // Mostrar nombre y descripción de la máscara
        maskNameText.text = maskData.maskName;
        descriptionText.text = "Descripcion: " + (maskData.name ?? "No disponible");

        // Mostrar los efectos de la máscara
        effectsText.text = "Efectos:";
        foreach (var effectData in maskData.effects)
        {
            effectsText.text += $"\n- {effectData.effect.name} (Valor: {effectData.value})";
        }

        // Hacer visible la UI de la notificación
        maskNameText.gameObject.SetActive(true);
        effectsText.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);

        // Esperar un tiempo antes de ocultar la notificación
        yield return new WaitForSeconds(5f); // Duración de la notificación en pantalla

        // Ocultar la UI después de la espera
        maskNameText.gameObject.SetActive(false);
        effectsText.gameObject.SetActive(false);
        descriptionText.gameObject.SetActive(false);
    }
}
