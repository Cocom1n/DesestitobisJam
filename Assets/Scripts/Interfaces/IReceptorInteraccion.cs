using UnityEngine;

/** Interfaz para entidades que pueden recibir objetos en diferentes sockets */
public interface IReceptorInteraccion
{
    /** Busca un punto de anclaje por su nombre ID */
    Transform ObtenerSocket(string nombre);

    /** Intenta ocupar un slot especifico. Devuelve true si esta libre y disponible. */
    bool IntentarOcupar(string nombre, IRecolectable item);

    /** Acceso al controlador de efectos del receptor */
    EffectController ObtenerEfectos();
}
