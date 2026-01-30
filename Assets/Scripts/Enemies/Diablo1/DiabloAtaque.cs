using UnityEngine;
using System.Collections;

public class DiabloAtaque : MonoBehaviour
{
    [SerializeField] private SogaAtaque soga;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float cooldown = 2f;

    private bool enCooldown;
    [SerializeField] private SerElectrocutado ScriptJugador;
    [SerializeField] private InteractorJugador interactorJugador;

    private void Start()
    {
        // suscribirse al evento AlMorir
        if (ScriptJugador != null)
        {
            ScriptJugador.AlMorir += OnJugadorMuerto;
        }
    }

    private void OnJugadorMuerto()
    {
        interactorJugador.PerderTodosLosObjetos();
    }

    public void IntentarAtacar(Transform puntoMano, IAgarraObjetos objetivo)
    {
        if (enCooldown || objetivo == null) return;

        // Antes de atacar, verificar si el objetivo tiene un objeto en la mano
        if (objetivo.TieneObjeto)
        {
            // Soltar el objeto y aplicar el mensaje de -1 de Hielo
            //objetivo.PerderObjeto();
        }

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
