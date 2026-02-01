using UnityEngine;

/** Clase base abstracta para efectos ScriptableObject */
public abstract class EffectBase : ScriptableObject, IEffect
{
    /** Aplica el efecto al objetivo con un valor especifico */
    public abstract void Apply(IEffectTarget target, float value);

    /** Remueve el efecto del objetivo con un valor especifico */
    public abstract void Remove(IEffectTarget target, float value);
}