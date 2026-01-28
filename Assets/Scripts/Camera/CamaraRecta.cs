using UnityEngine;

public class CamaraRecta : MonoBehaviour
{
    [Header("Configuracion de Objetivo")]
    [SerializeField] private Transform objetivo;
    [SerializeField] private Vector3 desfase = new Vector3(0, 5, -7); 
    [SerializeField] private float tiempoSuavizado = 0.12f;
    
    [SerializeField] private Vector3 rotacionFija = new Vector3(20, 0, 0); 

    private Vector3 velocidadActual;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(rotacionFija);
    }

    private void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 _posicionDestino = objetivo.position + desfase;
        
        transform.position = Vector3.SmoothDamp(transform.position, _posicionDestino, ref velocidadActual, tiempoSuavizado);
    }
}
