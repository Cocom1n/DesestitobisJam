using UnityEngine;

public class ElectrocutarEnArea : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private GameObject areaElectrocucion;
    [SerializeField] private GameObject particulas;
    [SerializeField] private float intervaloElectrocucion = 5f;
    [SerializeField] private float duracionArea = 0.5f;

    private float temporizador;

    void Start()
    {
        temporizador = intervaloElectrocucion;
        
        if (areaElectrocucion != null)
        {
            areaElectrocucion.SetActive(false);
            particulas.SetActive(false);
        }
    }

    void Update()
    {
        temporizador -= Time.deltaTime;

        if (temporizador <= 0f)
        {
            ActivarAreaElectrocucion();
            temporizador = intervaloElectrocucion;
        }
    }

    private void ActivarAreaElectrocucion()
    {
        if (areaElectrocucion != null)
        {
            areaElectrocucion.SetActive(true);
            particulas.SetActive(true);
            Invoke(nameof(DesactivarAreaElectrocucion), duracionArea);
        }
    }

    private void DesactivarAreaElectrocucion()
    {
        if (areaElectrocucion != null)
        {
            areaElectrocucion.SetActive(false);
            particulas.SetActive(false);
        }
    }
}

