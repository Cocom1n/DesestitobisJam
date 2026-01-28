using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/** Controla la aplicacion de efectos en un objetivo */
public class EffectController : MonoBehaviour
{
    /** Objetivo sobre el que se aplican los efectos */
    private IEffectTarget target;

    /** Lista de efectos temporales activos para evitar duplicados o gestionar limpieza */
    private readonly List<ActiveEffectInfo> activeTimedEffects = new();

    /** Efectos permanentes que deben revertirse al soltar la mascara */
    private readonly List<ActiveEffectInfo> reversibleEffects = new();

    /** Eventos para el HUD */
    public System.Action<MaskData> OnMaskEquipped;
    public System.Action OnMaskUnequipped;

    private void Awake()
    {
        target = GetComponent<IEffectTarget>();
    }

    /** Aplica todos los efectos de una mascara */
    public void ApplyMask(MaskData mask)
    {
        if (mask == null) return;

        foreach (var effectData in mask.effects)
        {
            ApplyEffect(effectData);
        }

        OnMaskEquipped?.Invoke(mask);
    }

    /** Remueve los efectos reversibles de la mascara actual */
    public void RemoveMaskEffects()
    {
        foreach (var info in reversibleEffects)
        {
            info.effect.Remove(target, info.value);
        }
        reversibleEffects.Clear();
        
        OnMaskUnequipped?.Invoke();
    }

    /** Gestiona la aplicacion de un unico efecto basado en su configuracion */
    private void ApplyEffect(MaskEffectData data)
    {
        if (data.effect == null) return;

        data.effect.Apply(target, data.value);

        ActiveEffectInfo info = new ActiveEffectInfo(data.effect, data.value);

        // Si es temporal (>0)
        if (data.duration > 0)
        {
            StartCoroutine(RunTimedEffect(info, data.duration));
        }
        // Si es permanente mientras se tenga la mascara (0) y es reversible
        else if (data.duration == 0 && data.reversibleOnDrop)
        {
            reversibleEffects.Add(info);
        }
        // Si es -1 (Instantaneo y permanente) no se agrega a ninguna lista de limpieza
    }

    /** Corrutina para efectos con tiempo limitado */
    private IEnumerator RunTimedEffect(ActiveEffectInfo info, float duration)
    {
        yield return new WaitForSeconds(duration);
        info.effect.Remove(target, info.value);
    }

    /** Clase auxiliar para trackear efectos aplicados */
    private class ActiveEffectInfo
    {
        public EffectBase effect;
        public float value;

        public ActiveEffectInfo(EffectBase e, float v)
        {
            effect = e;
            value = v;
        }
    }
}
