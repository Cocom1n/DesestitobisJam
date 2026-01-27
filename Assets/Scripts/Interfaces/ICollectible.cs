using UnityEngine;

/** Interfaz para objetos que pueden ser recolectados */
public interface ICollectible
{
    void Recolectar(Transform puntoAnclaje);
    GameObject ObtenerGameObject();
}
