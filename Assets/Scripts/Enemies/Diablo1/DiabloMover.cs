using UnityEngine;

public class DiabloMover : MonoBehaviour
{
    [SerializeField] private Transform[] puntosPatrulla;
    [SerializeField] private float velocidad = 4.5f;
    [SerializeField] private float disCambio = 0.5f;

    [Header("Baile")]
    [SerializeField] private float amplitud = 4.8f;
    [SerializeField] private float frecuencia = 1.15f;

    private int indice;
    private float tiempoBaile;

    public Vector3 Patrullar()
    {
        if (puntosPatrulla == null || puntosPatrulla.Length == 0) return Vector3.zero; ;

        Vector3 _pos = transform.position;
        Vector3 _dest = puntosPatrulla[indice].position;
        _dest.y = _pos.y;

        Vector3 _dir = _dest - _pos;

        if (_dir.sqrMagnitude <= disCambio * disCambio)
        {
            indice = (indice + 1) % puntosPatrulla.Length;
            return Vector3.zero;
        }

        _dir.Normalize();
        _dest += CalcularBaile(_dir);

        Vector3 _nuevaPos = Vector3.MoveTowards(
            _pos,
            _dest,
            velocidad * Time.deltaTime
        );

        transform.position = _nuevaPos;
        return _dir;
    }
    private Vector3 CalcularBaile(Vector3 direccion)
    {
        tiempoBaile += Time.deltaTime * frecuencia;
        return Vector3.Cross(direccion, Vector3.up) *
               (Mathf.Sin(tiempoBaile) * amplitud);
    }
}
