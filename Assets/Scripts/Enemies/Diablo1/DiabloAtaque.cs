using UnityEngine;
using System.Collections;

public class DiabloAtaque : MonoBehaviour
{
    [SerializeField] private SogaAtaque soga;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float cooldown = 2f;

    private bool enCooldown;

    public void IntentarAtacar(Transform puntoMano, IAgarraObjetos objetivo)
    {
        if (enCooldown || objetivo == null) return;
        StartCoroutine(Atacar(puntoMano, objetivo));
    }

    private IEnumerator Atacar(Transform puntoMano, IAgarraObjetos objetivo)
    {
        enCooldown = true;

        soga.LanzarSoga(puntoDisparo.position, puntoMano, objetivo);

        yield return new WaitForSeconds(cooldown);
        enCooldown = false;
    }
}
