using UnityEngine;

/** Sensor para objetos mediante un cono de vision */
public class SensorVision : MonoBehaviour
{
    [Header("Configuracion de Vision")]
    [SerializeField] private float rango = 1.5f;
    [SerializeField] private float anguloHorizontal = 80f;
    [SerializeField] private float anguloVertical = 65f;
    [SerializeField] private LayerMask obstaculos;
    [SerializeField] private LayerMask objetivos;

    private readonly Collider[] buffer = new Collider[16];

    // Cacheo xd
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

    /** Busca un objetivo IAgarraObjetos */
    public IAgarraObjetos DetectarObjetivo(out Transform puntoMano)
    {
        puntoMano = null;

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
            if (!buffer[i].TryGetComponent(out IAgarraObjetos objetivo))
                continue;

            if (!objetivo.TieneObjeto)
                continue;

            Transform mano = objetivo.ObtenerPuntoMano();
            Vector3 toTarget = mano.position - origen;

            // Distancia
            if (toTarget.sqrMagnitude > rangoSqr)
                continue;

            Vector3 dir = toTarget.normalized;

            // Horizontal
            Vector3 dirPlano = Vector3.ProjectOnPlane(dir, up).normalized;
            if (Vector3.Dot(dirPlano, forward) < cosHalfH)
                continue;

            // Vertical
            Vector3 dirVertical = Vector3.ProjectOnPlane(dir, right).normalized;
            if (Vector3.Dot(dirVertical, forward) < cosHalfV)
                continue;

            if (Physics.Raycast(origen, dir, toTarget.magnitude, obstaculos))
                continue;

            puntoMano = mano;
            return objetivo;
        }

        return null;
    }

    /** Comprueba si una posicion esta dentro de la piramide de vision ajustable */
    private bool EstaEnVision(Vector3 posicionObjetivo)
    {
        Vector3 _direccionLocal = transform.InverseTransformPoint(posicionObjetivo);
        
        if (_direccionLocal.z <= 0) return false;

        float _anguloH = Mathf.Atan2(_direccionLocal.x, _direccionLocal.z) * Mathf.Rad2Deg;
        float _anguloV = Mathf.Atan2(_direccionLocal.y, _direccionLocal.z) * Mathf.Rad2Deg;

        return Mathf.Abs(_anguloH) <= anguloHorizontal / 2f && 
               Mathf.Abs(_anguloV) <= anguloVertical / 2f;
    }

    /** Verifica si hay algo bloqueando la vista especifica hacia la mano */
    private bool TieneVisionLimpia(Vector3 posicionMano)
    {
        Vector3 _origen = transform.position;
        Vector3 _direccion = posicionMano - _origen;
        float _distancia = _direccion.magnitude;

        /** 
         * Rayo hacia la mano. Si golpeamos algo antes de llegar (otra persona)
         * la vision no es limpia. TMb hay un margen para evitar errores de precision
         */
        if (Physics.Raycast(_origen, _direccion.normalized, out RaycastHit _hit, _distancia + 0.1f, obstaculos))
        {
            /** Si el rayo choco con algo antes de llegar a la mano (distancia menor), esta bloqueado */
            if (_hit.distance < _distancia - 0.1f)
            {
                return false;
            }
        }
        
        return true;
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
