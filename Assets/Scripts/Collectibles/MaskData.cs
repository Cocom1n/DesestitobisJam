using UnityEngine;
using System.Collections.Generic;

/** Datos de la mascara: lista de efectos que aplica y su tiempo de vida */
[CreateAssetMenu(fileName = "NuevaMascara", menuName = "Colectables/Mascara")]
public class MaskData : ScriptableObject
{
    /** Nombre de la mascara */
    public string maskName;

    /** Icono para el HUD */
    public Sprite icon;

    /** Tiempo de vida total en segundos. 0 = Permanente (mientras se tenga) */
    public float lifetime;

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

    /** Si es true, el efecto no se quita al soltar y solo se aplica una vez jamas */
    public bool isPermanent;
}