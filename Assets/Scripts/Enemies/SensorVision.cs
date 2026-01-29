using UnityEngine;

/** Sensor para detectar objetivos mediante un cono de vision ajustable */
public class SensorVision : MonoBehaviour
{
    [Header("Configuracion de Vision")]
    [SerializeField] private float rango = 1.5f;
    [SerializeField] private float anguloHorizontal = 80f;
    [SerializeField] private float anguloVertical = 65f;
    [SerializeField] private LayerMask obstaculos;
    [SerializeField] private LayerMask objetivos;

    /** Buffer para evitar Garbage Collection */
    private readonly Collider[] buffer = new Collider[16];

    /** Cache de calculos */
    private float rangoSqr;
    private float cosHalfH;
    private float cosHalfV;

    private Transform tr;

    private void Awake()
    {
        tr = transform;

        rangoSqr = rango * rango;
        cosHalfH = Mathf.Cos(anguloHorizontal * 0.5f * Mathf.Deg2Rad);
        cosHalfV = Mathf.Cos(anguloVertical * 0.5f * Mathf.Deg2Rad);
    }

    /** Busca un objetivo que implemente IAgarraObjetos y tenga un objeto */
    public IAgarraObjetos DetectarObjetivo(out Transform puntoMano)
    {
        puntoMano = null;

        /** Deteccion inicial por area */
        int cantidad = Physics.OverlapSphereNonAlloc(
            tr.position,
            rango,
            buffer,
            objetivos
        );

        Vector3 forward = tr.forward;
        Vector3 up = tr.up;
        Vector3 right = tr.right;
        Vector3 origen = tr.position;

        for (int i = 0; i < cantidad; i++)
        {
            /** Intentar obtener el componente de agarre */
            if (!buffer[i].TryGetComponent(out IAgarraObjetos objetivo))
                continue;

            Transform mano = objetivo.ObtenerPuntoMano();
            if (mano == null)
                continue;

            Vector3 toTarget = mano.position - origen;

            /** Comprobacion de distancia al cuadrado (mas eficiente) */
            if (toTarget.sqrMagnitude > rangoSqr)
                continue;

            Vector3 dir = toTarget.normalized;

            /** Comprobacion de Cono Horizontal */
            Vector3 dirPlano = Vector3.ProjectOnPlane(dir, up).normalized;
            if (Vector3.Dot(dirPlano, forward) < cosHalfH)
                continue;

            /** Comprobacion de Cono Vertical */
            Vector3 dirVertical = Vector3.ProjectOnPlane(dir, right).normalized;
            if (Vector3.Dot(dirVertical, forward) < cosHalfV)
                continue;

            /** Raycast final para asegurar vision limpia */
            if (Physics.Raycast(origen, dir, toTarget.magnitude, obstaculos))
                continue;

            puntoMano = mano;

            return objetivo;
        }

        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.matrix = transform.localToWorldMatrix;

        float halfH = anguloHorizontal * 0.5f * Mathf.Deg2Rad;
        float halfV = anguloVertical * 0.5f * Mathf.Deg2Rad;

        float ancho = Mathf.Tan(halfH) * rango;
        float alto = Mathf.Tan(halfV) * rango;

        Vector3 v1 = new(-ancho, -alto, rango);
        Vector3 v2 = new(ancho, -alto, rango);
        Vector3 v3 = new(ancho, alto, rango);
        Vector3 v4 = new(-ancho, alto, rango);

        Gizmos.DrawLine(Vector3.zero, v1);
        Gizmos.DrawLine(Vector3.zero, v2);
        Gizmos.DrawLine(Vector3.zero, v3);
        Gizmos.DrawLine(Vector3.zero, v4);

        Gizmos.DrawLine(v1, v2);
        Gizmos.DrawLine(v2, v3);
        Gizmos.DrawLine(v3, v4);
        Gizmos.DrawLine(v4, v1);
    }
}
