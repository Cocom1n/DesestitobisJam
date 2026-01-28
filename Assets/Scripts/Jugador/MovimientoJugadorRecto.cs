using UnityEngine;

public class MovimientoJugadorRecto : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private Transform camara;
    public Vector3 CalcularDireccion(Vector2 input)
{
    // Ahora el movimiento es relativo al mundo, no a la cámara
    // input.y (W/S) mueve en Z, input.x (A/D) mueve en X
    Vector3 direccion = new Vector3(input.x, 0, input.y);
    
    return direccion.normalized * velocidad;
}
}
