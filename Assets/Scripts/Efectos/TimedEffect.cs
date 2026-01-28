using UnityEngine;

/** Clase base de efecto que se instancia desde las mascaras. 
 *  Ahora es stateless, el valor lo recibe por parametro.
 */
public abstract class TimedEffectBase : EffectBase
{
    /** Duracion del efecto. Se mantiene la firma por compatibilidad pero se remueve el estado interno */
    public abstract override void Apply(IEffectTarget target, float value);
    public abstract override void Remove(IEffectTarget target, float value);
}

/** Aumenta velocidad */
[CreateAssetMenu(fileName = "SpeedEffect", menuName = "Effects/SpeedEffect")]
public class SpeedEffect : TimedEffectBase
{
    public override void Apply(IEffectTarget target, float value)
    {
        target.SetTopSpeed(target.GetTopSpeed() + value);
    }

    public override void Remove(IEffectTarget target, float value)
    {
        target.SetTopSpeed(target.GetTopSpeed() - value);
    }
}

/** Aumenta vida */
[CreateAssetMenu(fileName = "HealthEffect", menuName = "Effects/HealthEffect")]
public class HealthEffect : TimedEffectBase
{
    public override void Apply(IEffectTarget target, float value)
    {
        target.SetLives(target.GetLives() + (int)value);
    }

    public override void Remove(IEffectTarget target, float value)
    {
        target.SetLives(target.GetLives() - (int)value);
    }
}

/** Aumenta danio */
[CreateAssetMenu(fileName = "DamageEffect", menuName = "Effects/DamageEffect")]
public class DamageEffect : TimedEffectBase
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
