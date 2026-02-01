using UnityEngine;

/** Aumenta vida */
[CreateAssetMenu(fileName = "VidaEffect", menuName = "Effects/VidaEffect")]
public class VidaEffect : TimedEffectBase
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