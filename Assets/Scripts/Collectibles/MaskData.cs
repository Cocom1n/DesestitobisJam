using UnityEngine;
using System.Collections.Generic;

/** Datos de la mascara: lista de efectos que aplica */
[CreateAssetMenu(fileName = "NuevaMascara", menuName = "Colectables/Mascara")]
public class MaskData : ScriptableObject
{
    /** Nombre de la mascara */
    public string maskName;

    /** Icono para el HUD */
    public Sprite icon;

    /** Lista de efectos parametrizados */
    public List<MaskEffectData> effects;
}

/** Datos de un efecto aplicado por la mascara */
[System.Serializable]
public class MaskEffectData
{
    /** Referencia al efecto base (SO) */
    public EffectBase effect;

    /** Valor del efecto */
    public float value;

    /** Duracion del efecto en segundos. 
     *  0 = Permanente (mientras se tenga la mascara)
     *  -1 = Instantaneo y Permanente (no se quita jamas)
     *  >0 = Temporal (se quita tras X segundos)
     */
    public float duration;

    /** Indica si el efecto debe revertirse al soltar la mascara (solo para duration >= 0) */
    public bool reversibleOnDrop = true;
}