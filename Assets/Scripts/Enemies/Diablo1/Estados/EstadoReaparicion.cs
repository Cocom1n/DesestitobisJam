using UnityEngine;
using System.Collections;

/** Estado donde el Diablo reaparece en una posicion aleatoria */
public class EstadoReaparicion : EstadoDiablo
{
    private float tiempoEspera = 2f; 
    public override void Entrar()
    {
        cerebro.StartCoroutine(ReaparecerYPatrullar());
    }

    private IEnumerator ReaparecerYPatrullar()
    {
        Reaparecer();
        yield return new WaitForSeconds(1f);
        cerebro.CambiarEstado(cerebro.EstadoPatrulla);
    }

    private void Reaparecer()
    {
        // Lógica para que el Diablo reaparezca en una posición aleatoria dentro de los puntos de patrullaje
        Transform[] puntosPatrulla = cerebro.Movimiento.GetPuntosPatrulla();
        if (puntosPatrulla.Length > 0)
        {
            Transform puntoAleatorio = puntosPatrulla[Random.Range(0, puntosPatrulla.Length)];
            cerebro.transform.position = puntoAleatorio.position;
        }
    }

    public override void Salir()
    {
        //cerebro.Movimiento.InitPatrulla();
    }
}
