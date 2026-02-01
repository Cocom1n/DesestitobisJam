using UnityEngine;

/** Aumenta danio */
[CreateAssetMenu(fileName = "CantBombEffect", menuName = "Effects/Cantidad de Bombuchas")]
public class CantBombEffect : TimedEffectBase
{
    public override void Apply(IEffectTarget target, float value)
    {
        target.SetDamage(target.GetDamage() + value);
    }

    public override void Remove(IEffectTarget target, float value)
    {
        target.SetDamage(target.GetDamage() - value);
    }
}