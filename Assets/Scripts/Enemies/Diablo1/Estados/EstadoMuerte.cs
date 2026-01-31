using UnityEngine;
using System.Collections;

/** Estado donde el Diablo esta muerto y espera para reaparecer */
public class EstadoMuerte : EstadoDiablo
{
    private float tiempoQuieto = 3f;  
    private float tiempoEspera = 2f; 

    public override void Entrar()
    {
        cerebro.Movimiento.DetenerPatrullaje();
        cerebro.Movimiento.gameObject.SetActive(false);
        cerebro.StartCoroutine(MuerteYReaparicion());  
    }

    private IEnumerator MuerteYReaparicion()
    {
        yield return new WaitForSeconds(tiempoQuieto);

        cerebro.Movimiento.gameObject.SetActive(false);

        yield return new WaitForSeconds(tiempoEspera);

        cerebro.CambiarEstado(cerebro.EstadoReaparicion);
    }

    public override void Salir()
    {
        cerebro.Movimiento.gameObject.SetActive(true);
    }
}
