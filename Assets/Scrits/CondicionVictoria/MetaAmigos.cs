using UnityEngine;

public class MetaAmigos : MonoBehaviour
{
    [SerializeField] private CondicionVictoria condicionVictoria;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (condicionVictoria != null)
            {
                condicionVictoria.MostrarVictoria();
            }
        }
    }
}
