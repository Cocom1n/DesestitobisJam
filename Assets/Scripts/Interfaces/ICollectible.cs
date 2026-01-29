using UnityEngine;

/** Interfaz para objetos que pueden ser recolectados */
public interface ICollectible
{
    /** Se llama cuando el objeto es agarrado */
    void Recolectar(Transform punto);
    
    /** Se llama cuando el objeto es soltado */
    void Soltar();
    
    /** Devuelve la referencia al GameObject del item */
    GameObject ObtenerGameObject();
}
