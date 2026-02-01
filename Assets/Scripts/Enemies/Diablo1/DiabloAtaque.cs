using UnityEngine;
using System.Collections;

public class DiabloAtaque : MonoBehaviour
{
    [SerializeField] private SogaAtaque soga;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float cooldown = 2f;

    private bool enCooldown;
    private SerElectrocutado ScriptJugador;
    private InteractorJugador interactorJugador;

    private void Start()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

        ScriptJugador = jugador.GetComponent<SerElectrocutado>();
        interactorJugador = jugador.GetComponent<InteractorJugador>();

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
        if (enCooldown || objetivo == null)
        {
            //Debug.LogWarning("El objetivo es null, no se puede atacar.");
            return;
        }

        // Antes de atacar, verificar si el objetivo tiene un objeto en la mano
        if (objetivo is SerElectrocutado jugador)
        {
            jugador.SoltarHielos();
            //objetivo.PerderObjeto();
        }

        StartCoroutine(Atacar(puntoMano, objetivo));
    }

    private IEnumerator Atacar(Transform puntoMano, IAgarraObjetos objetivo)
    {
        enCooldown = true;

        soga.LanzarSoga(puntoDisparo.position, puntoMano, objetivo);
        Debug.Log("Golpeando al " + objetivo);

        yield return new WaitForSeconds(cooldown);
        enCooldown = false;
    }
}
