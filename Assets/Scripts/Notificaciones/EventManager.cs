using System;
using UnityEngine;

public static class EventManager
{
    // Definir un evento estático que otros scripts pueden suscribir
    public static event Action<MaskData> OnMaskCollected = delegate { };

    // Llamar a este método cuando una máscara es recogida
    public static void TriggerMaskCollected(MaskData maskData)
    {
        OnMaskCollected?.Invoke(maskData);
    }
}
