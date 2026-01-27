using UnityEngine;

/** Implementacion base de un objeto que el jugador puede recoger */
[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class CollectibleItem : MonoBehaviour, ICollectible
{
    private Rigidbody cuerpoRigido;
    private Collider colisionador;
    private bool estaRecolectado;

    private void Awake()
    {
        cuerpoRigido = GetComponent<Rigidbody>();
        colisionador = GetComponent<Collider>();
        
        cuerpoRigido.isKinematic = false;
        colisionador.isTrigger = false;
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

        Debug.Log($"Objeto {gameObject.name} recolectado!");
    }

    public GameObject ObtenerGameObject()
    {
        return gameObject;
    }
}
