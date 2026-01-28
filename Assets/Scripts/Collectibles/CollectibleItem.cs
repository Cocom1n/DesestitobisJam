using UnityEngine;

/** Implementacion base de un objeto que el jugador puede recoger y soltar */
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class CollectibleItem : MonoBehaviour, ICollectible
{
    private Rigidbody cuerpoRigido;
    private Collider colisionador;
    private bool estaRecolectado;

    private void Awake()
    {
        cuerpoRigido = GetComponent<Rigidbody>();
        colisionador = GetComponent<Collider>();
        
        /** Aseguramos estado inicial fisico */
        cuerpoRigido.isKinematic = false;
        colisionador.enabled = true;
    }

    /** Desactiva la fisica y emparenta el objeto al punto de anclaje */
    public void Recolectar(Transform puntoAnclaje)
    {
        if (estaRecolectado) return;

        estaRecolectado = true;
        cuerpoRigido.isKinematic = true;
        colisionador.enabled = false;

        transform.SetParent(puntoAnclaje);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Debug.Log($"Objeto {gameObject.name} recolectado");
    }

    /** Prepara el objeto para volver a ser recolectable despues de caer */
    public void Soltar()
    {
        if (!estaRecolectado) return;

        estaRecolectado = false;
        transform.SetParent(null);
        
        cuerpoRigido.isKinematic = false;
        colisionador.enabled = true;
        
        Debug.Log($"Objeto {gameObject.name} soltado");
    }

    public GameObject ObtenerGameObject()
    {
        return gameObject;
    }
}
