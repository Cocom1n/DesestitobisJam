using UnityEngine;

public class MovimientoJugadorRecto : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private Transform camara;
    public Vector3 CalcularDireccion(Vector2 input)
{
    Vector3 direccion = new Vector3(input.x, 0, input.y);
    
    return direccion.normalized * velocidad;
}
}
