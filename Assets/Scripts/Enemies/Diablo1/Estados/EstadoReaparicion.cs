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
        yield return new WaitForSeconds(tiempoEspera);

        cerebro.CambiarEstado(cerebro.EstadoPatrulla);
    }

    private void Reaparecer()
    {
        Transform[] puntosPatrulla = cerebro.Movimiento.puntosPatrulla;
        if (puntosPatrulla.Length > 0)
        {
            Transform puntoAleatorio = puntosPatrulla[Random.Range(0, puntosPatrulla.Length)];
            cerebro.Movimiento.transform.position = puntoAleatorio.position;
        }
    }

    public override void Salir()
    {
        cerebro.Movimiento.InitPatrulla();
    }
}
