using UnityEngine;

/** Estado base para la IA del Diablo */
public abstract class EstadoDiablo : MonoBehaviour
{
    protected DiabloIA cerebro;

    public virtual void Inicializar(DiabloIA cerebroIA)
    {
        cerebro = cerebroIA;
    }

    /** Se llama al entrar en el estado */
    public virtual void Entrar() { }

    /** Se llama cada frame */
    public virtual void Actualizar() { }

    /** Se llama al salir del estado */
    public virtual void Salir() { }
}
