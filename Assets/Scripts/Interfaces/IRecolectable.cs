using UnityEngine;

/** Interfaz para objetos que pueden ser recolectados */
public interface IRecolectable
{
    /** Los objetos solicitan su anclaje al receptor mediante su identificador de socket */
    bool SerRecogido(IReceptorInteraccion receptor);

    /** Se llama para realizar el emparentamiento fisico */
    void Recolectar(Transform punto);

    /** Libera al objeto */
    void Soltar();

    /** Referencia al GameObject principal */
    GameObject ObtenerGameObject();
}
