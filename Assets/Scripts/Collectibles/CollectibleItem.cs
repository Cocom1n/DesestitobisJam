using UnityEngine;

/** Implementacion base de un objeto que el jugador puede recoger y soltar */
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class CollectibleItem : MonoBehaviour, ICollectible
{
    protected Rigidbody rb;
    protected Collider col;
    private bool estaRecolectado;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        
        /** Aseguramos estado inicial fisico */
        if (rb != null) rb.isKinematic = false;
        if (col != null) col.enabled = true;
    }

    /** Desactiva la fisica y emparenta el objeto al punto de anclaje */
    public virtual void Recolectar(Transform punto)
    {
        if (estaRecolectado) return;

        estaRecolectado = true;
        rb.isKinematic = true;
        col.enabled = false;

        transform.SetParent(punto);
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
        
        if (rb != null) rb.isKinematic = false;
        if (col != null) col.enabled = true;
        
        Debug.Log($"Objeto {gameObject.name} soltado");
    }

    public GameObject ObtenerGameObject()
    {
        return gameObject;
    }
}
