using UnityEngine;
using System.Collections.Generic;

/** Gestiona la deteccion e interaccion mediante una lista de sockets */
public class InteractorJugador : MonoBehaviour, IAgarraObjetos, IReceptorInteraccion
{
    [System.Serializable]
    public class SocketInfo
    {
        public string nombre;
        public Transform transform;
        public IRecolectable ocupante;
    }

    [Header("Configuracion de Deteccion")]
    [SerializeField] private float radioDeteccion = 0.5f;
    [SerializeField] private float alturaCapsula = 1.4f;
    [SerializeField] private float desfaseFrontal = 0.7f;
    [SerializeField] private float desfaseVertical = -0.7f;
    [SerializeField] private LayerMask capaRecolectables;

    [Header("Coleccion de Sockets")]
    [SerializeField] private List<SocketInfo> sockets;

    [Header("Configuracion de Desprendimiento")]
    [SerializeField] private float fuerzaImpactoMin = 2f;
    [SerializeField] private float fuerzaImpactoMax = 4f;
    [SerializeField] private float torqueImpacto = 10f;

    private EffectController effectController;

    private readonly Collider[] _bufferColisionadores = new Collider[5];
    private readonly HashSet<GameObject> _procesados;

    /** IAgarraObjetos: Indica si alguno de los sockets esta ocupado */
    public bool TieneObjeto
    {
        get
        {
            foreach (var s in sockets) if (s.ocupante != null) return true;
            return false;
        }
    }

    private void Awake()
    {
        effectController = GetComponent<EffectController>();
    }

    public void IntentarRecolectar()
    {
        _procesados.Clear();
        CalcularCapsula(out Vector3 b, out Vector3 s);
        int cant = Physics.OverlapCapsuleNonAlloc(b, s, radioDeteccion, _bufferColisionadores, capaRecolectables);

        for (int i = 0; i < cant; i++)
        {
            GameObject go = _bufferColisionadores[i].gameObject;
            if (_procesados.Contains(go)) continue;
            _procesados.Add(go);

            if (go.TryGetComponent(out IRecolectable item))
            {
                /** El objeto solicita su socket preferido */
                item.SerRecogido(this);
            }
        }
    }

    /** Implementacion IReceptorInteraccion: Busca el transform del socket por nombre */
    public Transform ObtenerSocket(string nombre)
    {
        foreach (var s in sockets)
        {
            if (s.nombre == nombre) return s.transform;
        }
        return null;
    }

    /** Implementacion IReceptorInteraccion: Gestiona la ocupacion del slot */
    public bool IntentarOcupar(string nombre, IRecolectable item)
    {
        foreach (var s in sockets)
        {
            if (s.nombre == nombre && s.ocupante == null)
            {
                s.ocupante = item;
                
                /** Logica especifica para liberar el slot si es una mascara */
                if (item is MaskCollectable mascara)
                {
                    mascara.OnMaskReleased += (m) => s.ocupante = null;
                }
                
                return true;
            }
        }
        return false;
    }

    public EffectController ObtenerEfectos() => effectController;

    public void SoltarPrioridad()
    {
        /** Buscamos soltar primero la mascara (Cabeza) para limpiar efectos */
        foreach (var s in sockets)
        {
            if (s.nombre == "Cabeza" && s.ocupante != null)
            {
                if (effectController != null)
                {
                    effectController.RemoveMaskEffects();
                }
                s.ocupante.Soltar();
                s.ocupante = null;
                return;
            }
        }

        /** Si no hay mascara, soltamos el primer objeto encontrado */
        foreach (var s in sockets)
        {
            if (s.nombre == "Mano" && s.ocupante != null)
            {
                //s.ocupante.Soltar();
                //s.ocupante = null;
                Debug.Log("No se puede soltar el OBJ de la mano");
                return;
            }
        }
    }

    /** IAgarraObjetos: El Diablo nos roba un objeto (Cabeza > Otros) */
    public void PerderObjeto()
    {
        SocketInfo victima = null;
        foreach (var s in sockets)
        {
            if (s.ocupante != null)
            {
                if (s.nombre == "Cabeza") { victima = s; break; }
                if (victima == null) victima = s;
            }
        }

        if (victima == null) return;

        if (victima.nombre == "Cabeza" && effectController != null)
        {
            effectController.RemoveMaskEffects();
        }

        GameObject go = victima.ocupante.ObtenerGameObject();
        victima.ocupante.Soltar();
        victima.ocupante = null;

        if (go != null && go.TryGetComponent(out Rigidbody rb))
        {
            Vector3 dir = (Vector3.up + Random.insideUnitSphere * 0.5f).normalized;
            rb.AddForce(dir * Random.Range(fuerzaImpactoMin, fuerzaImpactoMax), ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * torqueImpacto, ForceMode.Impulse);
        }
    }

    public GameObject ObtenerObjetoSostenido()
    {
        foreach (var s in sockets) if (s.ocupante != null) return s.ocupante.ObtenerGameObject();
        return null;
    }

    public Transform ObtenerPuntoMano() => ObtenerSocket("Mano");

    private void CalcularCapsula(out Vector3 b, out Vector3 s)
    {
        b = transform.position + transform.forward * desfaseFrontal + transform.up * desfaseVertical;
        s = b + Vector3.up * alturaCapsula;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        CalcularCapsula(out Vector3 b, out Vector3 s);
        Gizmos.DrawWireSphere(b, radioDeteccion);
        Gizmos.DrawWireSphere(s, radioDeteccion);
    }
}
