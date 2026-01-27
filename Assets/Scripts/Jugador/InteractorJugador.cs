using UnityEngine;

/** Maneja la interaccion y recoleccion de objetos usando un volumen de capsula ajustable */
public class InteractorJugador : MonoBehaviour
{
    [Header("Configuracion de Capsula")]
    [SerializeField] private float radioDeteccion = 0.32f;
    [SerializeField] private float alturaCapsula = 1.4f;
    [SerializeField] private float desfaseFrontal = 0.7f;
    [SerializeField] private float desfaseVertical = -0.72f;
    [SerializeField] private LayerMask capaRecolectables;

    [Header("Referencias")]
    [SerializeField] private Transform puntoMano;

    private IPlayerInput entrada;
    private ICollectible objetoSostenido;
    
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
        if (entrada == null || objetoSostenido != null) return;

        if (entrada.InteraccionPresionada)
        {
            IntentarRecolectar();
        }
    }

    /** Busca objetos recolectables usando una capsula con posicion ajustable */
    private void IntentarRecolectar()
    {
        CalcularCapsula(out Vector3 puntoBase, out Vector3 puntoSuperior);

        int cantidad = Physics.OverlapCapsuleNonAlloc(
            puntoBase,
            puntoSuperior,
            radioDeteccion,
            _bufferColisionadores,
            capaRecolectables
        );

        for (int i = 0; i < cantidad; i++)
        {
            if (_bufferColisionadores[i].TryGetComponent(out ICollectible recolectable))
            {
                objetoSostenido = recolectable;
                objetoSostenido.Recolectar(puntoMano);
                return;
            }
        }
    }

    /** Calcula los puntos base y superior de la cápsula */
    private void CalcularCapsula(out Vector3 puntoBase, out Vector3 puntoSuperior)
    {
        puntoBase = transform.position
                    + transform.forward * desfaseFrontal
                    + transform.up * desfaseVertical;

        puntoSuperior = puntoBase + Vector3.up * alturaCapsula;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 _puntoBase = transform.position + (transform.forward * desfaseFrontal) + (transform.up * desfaseVertical);
        Vector3 _puntoSuperior = _puntoBase + (Vector3.up * alturaCapsula);

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
