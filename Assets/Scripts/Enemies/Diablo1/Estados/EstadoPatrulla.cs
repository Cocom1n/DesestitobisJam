using UnityEngine;

/** Estado donde el Diablo patrulla entre puntos bailando con ondulaciones */
public class EstadoPatrulla : EstadoDiablo
{
    public override void Actualizar()
    {
        /** Buscar objetivo usando los sensores */
        if (cerebro.BuscarObjetivo())
        {
            cerebro.CambiarEstado(cerebro.EstadoAtaque);
            return;
        }

        /** Movimiento normal de patrulla */
        Vector3 _dir = cerebro.Movimiento.Patrullar();
        if (_dir != Vector3.zero)
        {
            cerebro.Rotacion.RotarHacia(cerebro.transform.position + _dir);
        }
    }
}