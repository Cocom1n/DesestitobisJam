using UnityEngine;

/** Estado donde el Diablo ataca */
public class EstadoAtaque : EstadoDiablo
{
    public override void Actualizar()
    {
        if (!cerebro.BuscarObjetivo())
        {
            cerebro.CambiarEstado(cerebro.EstadoPatrulla);
            return;
        }
        /** Ataque con cooldown */
        cerebro.Ataque.IntentarAtacar(
            cerebro.PuntoManoObjetivo,
            cerebro.ObjetivoActual
        );

        /** Seguir moviendose mientras ataca */
        Vector3 _dir = cerebro.Movimiento.Patrullar();
        if (_dir != Vector3.zero)
        {
            cerebro.Rotacion.RotarHacia(cerebro.transform.position + _dir);
        }

    }
}
