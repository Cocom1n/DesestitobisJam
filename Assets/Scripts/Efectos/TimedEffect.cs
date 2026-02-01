using UnityEngine;

/** Clase base de efecto que se instancia desde las mascaras. 
 *  Ahora es stateless, el valor lo recibe por parametro.
 */
public abstract class TimedEffectBase : EffectBase
{
    // Hereda Apply y Remove de EffectBase sin necesidad de abstract override redundante
}
