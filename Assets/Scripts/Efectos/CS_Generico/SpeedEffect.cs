using UnityEngine;

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