using UnityEngine;

/** Interfaz para cualquier objeto que pueda sostener un item recolectable */
public interface IAgarraObjetos
{
    /** Indica si el poseedor tiene un objeto actualmente */
    bool TieneObjeto { get; }

    /** Permite que Otro le quite el objeto al poseedor */
    void PerderObjeto();
    
    /** Obtiene el objeto sostenido actual */
    GameObject ObtenerObjetoSostenido();

    /** Obtiene el punto de transformacion de la mano/agarre para chequeos de vision */
    Transform ObtenerPuntoMano();
}
