using UnityEngine;

/** Maneja la interaccion y recoleccion de objetos usando un volumen de capsula ajustable */
public class InteractorJugador : MonoBehaviour, IAgarraObjetos
{
    [Header("Configuracion de Capsula")]
    [SerializeField] private float radioDeteccion = 0.32f;
    [SerializeField] private float alturaCapsula = 1.4f;
    [SerializeField] private float desfaseFrontal = 0.7f;
    [SerializeField] private float desfaseVertical = -0.72f;
    [SerializeField] private LayerMask capaRecolectables;

    [Header("Configuracion de Desprendimiento")]
    [SerializeField] private float fuerzaImpactoMin = 2f;
    [SerializeField] private float fuerzaImpactoMax = 4f;
    [SerializeField] private float torqueImpacto = 10f;

    [Header("Referencias")]
    [SerializeField] private Transform puntoMano;

    private IPlayerInput entrada;
    private ICollectible objetoSostenido;
    
    /** Implementacion de IAgarraObjetos */
    public bool TieneObjeto => objetoSostenido != null;

    /** Buffer para evitar Garbage Collection (GC) */
    private readonly Collider[] _bufferColisionadores = new Collider[5];

    private void Awake()
    {
        entrada = GetComponent<IPlayerInput>();
        
        if (puntoMano == null)
        {
            puntoMano = transform;
        }
    }

    private void Update()
    {
        if (entrada == null) return;

        /** Solo intentamos recolectar si no tenemos nada en la mano */
        if (!TieneObjeto && entrada.InteraccionPresionada)
        {
            IntentarRecolectar();
        }
    }

    /** Busca objetos recolectables usando una capsula con posicion ajustable */
    private void IntentarRecolectar()
    {
        CalcularCapsula(out Vector3 _puntoBase, out Vector3 _puntoSuperior);

        int _cantidad = Physics.OverlapCapsuleNonAlloc(
            _puntoBase,
            _puntoSuperior,
            radioDeteccion,
            _bufferColisionadores,
            capaRecolectables
        );

        for (int _i = 0; _i < _cantidad; _i++)
        {
            if (_bufferColisionadores[_i].TryGetComponent(out ICollectible _recolectable))
            {
                objetoSostenido = _recolectable;
                objetoSostenido.Recolectar(puntoMano);
                return;
            }
        }
    }

    /** IAgarraObjetos: El Diablo llama a este metodo para quitar el objeto */
    public void PerderObjeto()
    {
        if (objetoSostenido == null) return;

        /** Guardar referencia temporal para aplicar fuerzas */
        GameObject _item = objetoSostenido.ObtenerGameObject();
        Rigidbody _rb = _item.GetComponent<Rigidbody>();

        /** Soltar mediante la interfaz del item */
        objetoSostenido.Soltar();
        objetoSostenido = null;

        /** Aplicar un golpe aleatorio al objeto, NO al jugador */
        if (_rb != null)
        {
            Vector3 _direccionAleatoria = (Vector3.up + Random.insideUnitSphere * 0.5f).normalized;
            float _fuerza = Random.Range(fuerzaImpactoMin, fuerzaImpactoMax);
            
            _rb.AddForce(_direccionAleatoria * _fuerza, ForceMode.Impulse);
            _rb.AddTorque(Random.insideUnitSphere * torqueImpacto, ForceMode.Impulse);
        }
    }

    public GameObject ObtenerObjetoSostenido()
    {
        return objetoSostenido != null ? objetoSostenido.ObtenerGameObject() : null;
    }

    public Transform ObtenerPuntoMano()
    {
        return puntoMano;
    }

    /** Calcula los puntos base y superior de la capsula REAL de forma correcta */
    private void CalcularCapsula(out Vector3 puntoBase, out Vector3 puntoSuperior)
    {
        /** USAR transform.position DIRECTAMENTE. CERO DIVISIONES POR 4. */
        puntoBase = transform.position
                    + transform.forward * desfaseFrontal
                    + transform.up * desfaseVertical;

        puntoSuperior = puntoBase + Vector3.up * alturaCapsula;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        CalcularCapsula(out Vector3 _puntoBase, out Vector3 _puntoSuperior);

        /** Dibujar volumen de la capsula */
        Gizmos.DrawWireSphere(_puntoBase, radioDeteccion);
        Gizmos.DrawWireSphere(_puntoSuperior, radioDeteccion);

        Vector3 _derecha = transform.right * radioDeteccion;
        Vector3 _adelante = transform.forward * radioDeteccion;

        Gizmos.DrawLine(_puntoBase + _derecha, _puntoSuperior + _derecha);
        Gizmos.DrawLine(_puntoBase - _derecha, _puntoSuperior - _derecha);
        Gizmos.DrawLine(_puntoBase + _adelante, _puntoSuperior + _adelante);
        Gizmos.DrawLine(_puntoBase - _adelante, _puntoSuperior - _adelante);
    }
}
