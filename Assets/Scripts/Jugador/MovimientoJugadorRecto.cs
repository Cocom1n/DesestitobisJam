using UnityEngine;

/** Maneja el movimiento recto del jugador sincronizado con PlayerStats */
public class MovimientoJugadorRecto : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Transform camara;

    private void Awake()
    {
        /** Obtener referencia a los stats si no esta asignada */
        if (stats == null)
        {
            stats = GetComponent<PlayerStats>();
        }
    }

    /** Calcula la direccion de movimiento usando la velocidad de PlayerStats */
    public Vector3 CalcularDireccion(Vector2 input)
    {
        // El movimiento es relativo al mundo
        // input.y (W/S) mueve en Z, input.x (A/D) mueve en X
        Vector3 direccion = new Vector3(input.y, 0, input.x*-1);
        
        /** Usar el stat topSpeed del jugador */
        float velocidadActual = stats != null ? stats.GetTopSpeed() : 5f;
        
        return direccion.normalized * velocidadActual;
    }
}
