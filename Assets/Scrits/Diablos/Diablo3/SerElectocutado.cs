using UnityEngine;
using System;

public class SerElectrocutado : MonoBehaviour, IElectrocutable
{
    [SerializeField] private ParticleSystem fxParticulasHielos;
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

        if (fxParticulasHielos != null)
        {
            fxParticulasHielos.Emit(1);
        }

        AlCambiarVida?.Invoke(vidaActual); 

        if (vidaActual <= 0)
        {
            Morir();
        }
        Debug.Log("vidaActual actual:" + vidaActual);
    }

    public void Morir()
    {
        if (fxParticulasHielos != null)
        {
            fxParticulasHielos.Emit(5);
        }
        AlMorir?.Invoke();
    }

    public int ObtenerVidaActual()
    {
        return vidaActual;
    }
}