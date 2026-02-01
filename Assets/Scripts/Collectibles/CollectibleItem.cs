using UnityEngine;

/** Implementacion base que busca el socket 'Mano' por defecto */
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class CollectibleItem : MonoBehaviour, IRecolectable
{
    [Header("Configuracion de Anclaje")]
    [SerializeField] protected string nombreSocketObjetivo = "Mano";

    protected Rigidbody rb;
    protected Collider col;
    protected bool estaRecolectado;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        
        rb.isKinematic = false;
        col.enabled = true;
    }

    /** Solicita al receptor el socket configurado */
    public virtual bool SerRecogido(IReceptorInteraccion receptor)
    {
        if (estaRecolectado || receptor == null) return false;

        Transform punto = receptor.ObtenerSocket(nombreSocketObjetivo);
        
        /** Si el receptor tiene el socket y lo permite ocupar, recolectamos */
        if (punto != null && receptor.IntentarOcupar(nombreSocketObjetivo, this))
        {
            Recolectar(punto);
            return true;
        }
        return false;
    }

    /** Anclaje fisico al transform obtenido */
    public virtual void Recolectar(Transform punto)
    {
        estaRecolectado = true;
        if (rb != null) rb.isKinematic = true;
        if (col != null) col.enabled = false;

        transform.SetParent(punto);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Debug.Log($"Objeto {gameObject.name} recolectado en socket: {punto.name}");
    }

    /** Prepara el objeto para volver a ser recolectable despues de caer */
    public virtual void Soltar()
    {
        if (!estaRecolectado) return;

        estaRecolectado = false;
        transform.SetParent(null);
        
        rb.isKinematic = false;
        col.enabled = true;
        col.isTrigger = false;
        rb.useGravity = true;
        Debug.Log($"Objeto {gameObject.name} soltado");
    }

    public GameObject ObtenerGameObject() => gameObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terreno"))
        {
            if (rb != null) { rb.isKinematic = true; rb.useGravity = false; }
            if (col != null) col.isTrigger = true;

            Debug.Log($"Objeto {gameObject.name} ha colisionado con el layer 'Default'");
        }
    }
}
