using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/** Controla la aplicacion de efectos y gestiona el tiempo de vida de las mascaras */
public class EffectController : MonoBehaviour
{
    private IEffectTarget target;

    /** Efectos que deben revertirse al soltar la mascara (no permanentes) */
    private readonly List<ActiveEffectInfo> revertibleEffects = new();

    /** Registro de efectos permanentes ya aplicados para no repetir */
    private readonly HashSet<EffectBase> appliedPermanentEffects = new();

    private MaskCollectable currentMaskItem;
    private Coroutine maskLifetimeCoroutine;

    /** Eventos HUD */
    public System.Action<MaskData> OnMaskEquipped;
    public System.Action OnMaskUnequipped;

    private void Awake()
    {
        target = GetComponent<IEffectTarget>();
    }

    /** Aplica efectos de mascara y gestiona su lifetime */
    public void ApplyMask(MaskData mask, MaskCollectable item)
    {
        if (mask == null || item == null) return;

        // Limpiar efectos previos si existian (no los permanentes ya registrados)
        RemoveMaskEffects();

        currentMaskItem = item;

        foreach (var data in mask.effects)
        {
            if (data.effect == null) continue;

            if (data.isPermanent)
            {
                // Solo aplicar si no se ha aplicado este efecto antes jamas
                if (!appliedPermanentEffects.Contains(data.effect))
                {
                    data.effect.Apply(target, data.value);
                    appliedPermanentEffects.Add(data.effect);
                    Debug.Log($"Efecto permanente {data.effect.name} aplicado");
                }
            }
            else
            {
                // Efecto temporal o ligado a la mascara
                data.effect.Apply(target, data.value);
                revertibleEffects.Add(new ActiveEffectInfo(data.effect, data.value));
            }
        }

        if (mask.lifetime > 0)
        {
            maskLifetimeCoroutine = StartCoroutine(RunMaskLifetime(mask.lifetime));
        }

        OnMaskEquipped?.Invoke(mask);
    }

    /** Revierte efectos no permanentes y detiene timers */
    public void RemoveMaskEffects()
    {
        if (maskLifetimeCoroutine != null)
        {
            StopCoroutine(maskLifetimeCoroutine);
            maskLifetimeCoroutine = null;
        }

        foreach (var info in revertibleEffects)
        {
            info.effect.Remove(target, info.value);
        }
        revertibleEffects.Clear();
        currentMaskItem = null;

        OnMaskUnequipped?.Invoke();
    }

    /** Corrutina para fin de vida de mascara temporal */
    private IEnumerator RunMaskLifetime(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (currentMaskItem != null)
        {
            currentMaskItem.Expirar();
        }

        // Al expirar una temporal, se quitan sus efectos reversibles
        RemoveMaskEffects();
    }

    private class ActiveEffectInfo
    {
        public EffectBase effect;
        public float value;
        public ActiveEffectInfo(EffectBase e, float v) { effect = e; value = v; }
    }
}
