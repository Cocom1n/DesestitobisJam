using UnityEngine;

/** Interfaz para efectos aplicables a un objetivo */
public interface IEffect
{
    /** Aplica el efecto al objetivo con un valor especifico */
    void Apply(IEffectTarget target, float value);

    /** Elimina el efecto del objetivo con un valor especifico */
    void Remove(IEffectTarget target, float value);
}
