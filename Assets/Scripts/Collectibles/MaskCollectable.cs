using UnityEngine;

/** Mascara recolectable que aplica todos sus efectos al jugador */
public class MaskCollectable : CollectibleItem
{
    /** Datos de la mascara */
    [SerializeField] private MaskData maskData;

    /** Aplica los efectos de la mascara al recolectar */
    public void RecolectarMask(Transform puntoMano, EffectController effects)
    {
        // Se corrigio la comprobacion invertida: ahora continua si ambos son validos
        if (maskData == null || effects == null) return;

        base.Recolectar(puntoMano); // Solo anclar fisica

        effects.ApplyMask(maskData);

        Debug.Log($"Mascara {maskData.maskName} recolectada");
    }
}
