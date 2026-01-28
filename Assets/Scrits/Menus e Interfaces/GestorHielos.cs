using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GestorHielos : MonoBehaviour
{
    [SerializeField] private SerElectrocutado jugadorLogica;

    [SerializeField] private GameObject hieloPrefab;
    [SerializeField] private Transform puntoGeneracion;
    [SerializeField] private TextMeshProUGUI textoContador;

    private List<GameObject> hielosEnVaso = new List<GameObject>();

    private void OnEnable()
    {
        if (jugadorLogica != null)
        {
            jugadorLogica.AlCambiarVida += ActualizarVaso;
            jugadorLogica.AlMorir += EfectoMuerteVisual;
        }
    }

    private void OnDisable()
    {
        if (jugadorLogica != null)
        {
            jugadorLogica.AlCambiarVida -= ActualizarVaso;
            jugadorLogica.AlMorir -= EfectoMuerteVisual;
        }
    }

    private void ActualizarVaso(int vidaActual)
    {
        if(textoContador != null) 
            textoContador.text = vidaActual.ToString();

        while (hielosEnVaso.Count < vidaActual)
        {
            CrearHieloFisico();
        }
        while (hielosEnVaso.Count > vidaActual)
        {
            EliminarHieloFisico();
        }
    }

    private void CrearHieloFisico()
    {
        GameObject nuevoHielo = Instantiate(hieloPrefab, puntoGeneracion.position, Quaternion.identity, puntoGeneracion.parent);
        
        nuevoHielo.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-25f, 25f));
        
        hielosEnVaso.Add(nuevoHielo);
    }

    private void EliminarHieloFisico()
    {
        if (hielosEnVaso.Count > 0)
        {
            int index = Random.Range(0, hielosEnVaso.Count);
            GameObject hielo = hielosEnVaso[index];
            
            hielosEnVaso.RemoveAt(index);
            Destroy(hielo);
        }
    }

    private void EfectoMuerteVisual()
    {
        Debug.Log("Visual: El vaso se ha quedado vacío.");
    }
}
