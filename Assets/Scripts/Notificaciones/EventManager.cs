using System;
using UnityEngine;

public static class EventManager
{
    public static event Action<MaskData> OnMaskCollected = delegate { };

    // Llamar cunado se recoje una mascara
    public static void TriggerMaskCollected(MaskData maskData)
    {
        OnMaskCollected?.Invoke(maskData);
    }
}
