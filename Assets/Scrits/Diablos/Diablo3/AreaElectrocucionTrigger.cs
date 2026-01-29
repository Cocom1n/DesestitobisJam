using UnityEngine;
using System.Collections.Generic;

public class AreaElectrocucionTrigger : MonoBehaviour
{
    private HashSet<GameObject> objetosDanados = new HashSet<GameObject>();

    private void OnEnable()
    {
        objetosDanados.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        AplicarElectrocucion(other);
    }

    private void OnTriggerStay(Collider other)
    {
        AplicarElectrocucion(other);
    }

    private void AplicarElectrocucion(Collider other)
    {
        IElectrocutable electrocutable = other.GetComponentInParent<IElectrocutable>();
        
        if (electrocutable == null) return;

        GameObject objetoConComponente = (electrocutable as Component).gameObject;
        
        if (objetosDanados.Contains(objetoConComponente)) return;

        electrocutable.SoltarHielos();
        objetosDanados.Add(objetoConComponente);
    }
}

