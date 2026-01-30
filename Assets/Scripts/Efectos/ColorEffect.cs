using UnityEngine;

/** Efecto visual que cambia el color del objetivo */
[CreateAssetMenu(fileName = "ColorEffect", menuName = "Effects/ColorEffect")]
public class ColorEffect : EffectBase
{
    [SerializeField] private Color effectColor = Color.blue;

    /** Aplica el color. El parametro 'value' no se usa aqui directamente, se usa el color configurado */
    public override void Apply(IEffectTarget target, float value)
    {
        target.SetColor(effectColor);
    }

    /** Remueve el color. En una implementacion mas compleja podria guardar el previo, 
     *  pero aqui asumimos que 'Remove' significa volver al original del target.
     */
    public override void Remove(IEffectTarget target, float value)
    {
        // Si el target es PlayerStats, podemos llamar a ResetColor.
        // Como IEffectTarget es generico, podriamos necesitar un SetColor(Color.white)
        target.SetColor(Color.white); 
    }
}
