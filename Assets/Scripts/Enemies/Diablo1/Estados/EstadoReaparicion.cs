using UnityEngine;
using System.Collections;

/** Estado donde el Diablo reaparece en una posicion aleatoria */
public class EstadoReaparicion : EstadoDiablo
{
    private float tiempoEspera = 2f;

    /******* METODOS *******/

    /** Metodo que se ejecuta al entrar al estado, inicia la corrutina de reaparecer y patrullar */
    public override void Entrar()
    {
        cerebro.StartCoroutine(ReaparecerYPatrullar());
    }

    /** Metodo que gestiona el proceso de reaparecer y luego cambia al estado de patrulla */
    private IEnumerator ReaparecerYPatrullar()
    {
        Reaparecer();
        yield return new WaitForSeconds(1f);
        cerebro.CambiarEstado(cerebro.EstadoPatrulla);
    }

    /** Metodo que hace que el Diablo reaparezca en una posicion aleatoria dentro de los puntos de patrullaje */
    private void Reaparecer()
    {
        Transform[] puntosPatrulla = cerebro.Movimiento.GetPuntosPatrulla();
        if (puntosPatrulla.Length > 0)
        {
            Transform puntoAleatorio = puntosPatrulla[Random.Range(0, puntosPatrulla.Length)];
            cerebro.transform.position = puntoAleatorio.position;
        }
    }

    /** Metodo que se ejecuta al salir del estado, aunque actualmente no hace nada */
    public override void Salir()
    {
        // cerebro.Movimiento.InitPatrulla(); 
    }
}
