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
    [SerializeField] private Transform puntoCabeza;


    private IPlayerInput entrada;
    private ICollectible objetoMano;
    private MaskCollectable mascaraEquipada;

    private EffectController effectController;

    // Enlazado con el diablo 1
    public bool TieneObjeto => objetoMano != null || mascaraEquipada != null;


    /** Buffer para evitar Garbage Collection (GC) */
    private readonly Collider[] _bufferColisionadores = new Collider[5];

    private void Awake()
    {
        entrada = GetComponent<IPlayerInput>();
        effectController = GetComponent<EffectController>();

        if (puntoMano == null) puntoMano = transform;
        if (puntoCabeza == null) puntoCabeza = transform;
    }

    private void Update()
    {
        if (entrada == null) return;

        /** Solo intentamos recolectar si no tenemos nada en la mano */
        if (entrada.InteraccionPresionada)
        {
            IntentarRecolectar();
        }
        /** Si tenemos algo y presionamos Q, lo soltamos y quitamos efectos */
        else if (entrada.SoltarMascaraPresionada)
        {
            DesequiparMascara();
        }
    }

    /** Busca objetos recolectables usando una capsula ajustable */
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

        for (int i = 0; i < _cantidad; i++)
        {
            if (_bufferColisionadores[i].TryGetComponent(out MaskCollectable _mascara))
            {
                // Solo agarrar si no hay nada en la cabeza
                if (mascaraEquipada == null && !_mascara.Expiro)
                {
                    mascaraEquipada = _mascara;

                    _mascara.transform.SetParent(puntoCabeza);
                    _mascara.transform.localPosition = Vector3.zero;

                    _mascara.RecolectarMask(effectController, puntoCabeza);
                    _mascara.OnMaskReleased += OnMascaraLiberada;
                }
                return; // un obj por actializacion

            }
            if (_bufferColisionadores[i].TryGetComponent(out ICollectible _item))
            {
                if (objetoMano == null)
                {
                    objetoMano = _item;
                    _item.Recolectar(puntoMano);
                }
                return;
            }
        }
    }
    private void OnMascaraLiberada(MaskCollectable mask)
    {
        if (mascaraEquipada == mask)
        {
            mascaraEquipada = null;
            effectController?.RemoveMaskEffects();
        }
        mask.OnMaskReleased -= OnMascaraLiberada;
    }


    /** Quita la mascara, revierte sus efectos y la suelta fisicamente */
    private void DesequiparMascara()
    {
        if (mascaraEquipada == null) return;

        /** Revertir efectos temporales/reversibles */
        effectController.RemoveMaskEffects();

        if (mascaraEquipada.MaskData.lifetime > 0)
            mascaraEquipada.Expirar();
        else
            mascaraEquipada.Soltar();

        mascaraEquipada = null;

        Debug.Log("Mascara desequipada");
    }

    /** Decide donde va el item */
    private Transform ObtenerPuntoEquipamiento(CollectibleItem item)
    {
        return item is MaskCollectable ? puntoCabeza : puntoMano;
    }

    /** IAgarraObjetos: El Diablo llama a este metodo para quitar el objeto */
    public void PerderObjeto()
    {
        PerderHielo();
        if (mascaraEquipada == null) return;

        /** Si el Diablo nos quita la mascara, tambien perdemos los efectos */
        effectController?.RemoveMaskEffects();

        GameObject _go = mascaraEquipada.ObtenerGameObject();
        if (_go == null) return;

        DesequiparMascara();

        if (!_go.TryGetComponent(out Rigidbody _rb)) return;

        Vector3 _direccion = (Vector3.up + Random.insideUnitSphere * 0.5f).normalized;
        float _fuerza = Random.Range(fuerzaImpactoMin, fuerzaImpactoMax);

        _rb.AddForce(_direccion * _fuerza, ForceMode.Impulse);
        _rb.AddTorque(Random.insideUnitSphere * torqueImpacto, ForceMode.Impulse);

        mascaraEquipada = null;
        Debug.Log("mascara empujada con fuerza");
    }

    public void PerderHielo()
    {
        if (objetoMano != null)
        {
            Debug.Log("quitar hielo");

        }
    }


    public Transform ObtenerPuntoMano() => puntoMano;
    public Transform ObtenerPuntoCabeza() => puntoCabeza;


    /** Calcula los puntos base y superior de la capsula */
    private void CalcularCapsula(out Vector3 puntoBase, out Vector3 puntoSuperior)
    {
        puntoBase = transform.position
                    + transform.forward * desfaseFrontal
                    + transform.up * desfaseVertical;

        puntoSuperior = puntoBase + Vector3.up * alturaCapsula;
    }
    public GameObject ObtenerObjetoSostenido()
    {
        if (mascaraEquipada != null)
            return mascaraEquipada.ObtenerGameObject();

        if (objetoMano != null)
            return objetoMano.ObtenerGameObject();

        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        CalcularCapsula(out Vector3 _puntoBase, out Vector3 _puntoSuperior);

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
