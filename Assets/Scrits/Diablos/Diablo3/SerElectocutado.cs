using UnityEngine;
using System;

public class SerElectrocutado : MonoBehaviour, IElectrocutable
{
    [SerializeField] private int vidaInicial = 5;
    private int vidaActual;

    public event Action<int> AlCambiarVida;
    public event Action AlMorir;

    void Start()
    {
        vidaActual = vidaInicial;
        AlCambiarVida?.Invoke(vidaActual); 
    }

    public void SoltarHielos()
    {
        if (vidaActual <= 0) return;

        vidaActual--;

        AlCambiarVida?.Invoke(vidaActual); 

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    public void Morir()
    {
        Debug.Log("Lógica: El jugador ha muerto.");
        AlMorir?.Invoke();
    }
}