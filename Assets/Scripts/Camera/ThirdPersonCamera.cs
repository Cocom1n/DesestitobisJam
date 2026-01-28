using UnityEngine;

/** Controla la camara en tercera persona siguiendo a un objetivo */
public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Configuracion de Objetivo")]
    [SerializeField] private Transform objetivo;
    [SerializeField] private Vector3 desfase = new Vector3(0, 2, -5);
    [SerializeField] private float tiempoSuavizado = 0.12f;

    [Header("Configuracion de Rotacion")]
    [SerializeField] private float sensibilidad = 2f;
    [SerializeField] private float inclinacionMinima = -30f;
    [SerializeField] private float inclinacionMaxima = 60f;

    private float rotacionY;
    private float rotacionX;
    private Vector3 velocidadActual;
    private IPlayerInput proveedorEntrada;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        if (objetivo != null)
        {
            proveedorEntrada = objetivo.GetComponent<IPlayerInput>();
        }
    }

    private void LateUpdate()
    {
        if (objetivo == null) return;

        ManejarRotacion();
        SeguirObjetivo();
    }

    /** Maneja la rotacion de la camara basada en la entrada del jugador */
    private void ManejarRotacion()
    {
        if (proveedorEntrada == null) return;

        Vector2 _entradaMirar = proveedorEntrada.EntradaMirar;
        rotacionY += _entradaMirar.x * sensibilidad;
        rotacionX -= _entradaMirar.y * sensibilidad;
        rotacionX = Mathf.Clamp(rotacionX, inclinacionMinima, inclinacionMaxima);

        transform.rotation = Quaternion.Euler(rotacionX, rotacionY, 0);
    }

    /** Posiciona la camara suavemente detras del objetivo */
    private void SeguirObjetivo()
    {
        Vector3 _posicionObjetivo = objetivo.position + transform.rotation * desfase;
        transform.position = Vector3.SmoothDamp(transform.position, _posicionObjetivo, ref velocidadActual, tiempoSuavizado);
    }

    /** Permite cambiar el objetivo de la camara en tiempo de ejecucion */
    public void EstablecerObjetivo(Transform nuevoObjetivo)
    {
        objetivo = nuevoObjetivo;
        if (objetivo != null)
        {
            proveedorEntrada = objetivo.GetComponent<IPlayerInput>();
        }
    }
}
