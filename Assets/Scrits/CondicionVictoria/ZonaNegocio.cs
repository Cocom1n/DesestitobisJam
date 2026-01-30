using UnityEngine;

public class ZonaNegocio : MonoBehaviour
{
    [SerializeField] private CondicionVictoria condicionVictoria;


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (condicionVictoria != null)
            {
                condicionVictoria.IniciarContador();
            }
        }
    }
}
