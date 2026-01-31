using UnityEngine;

/** Clase que representa una zona con sus respectivos puntos de spawn y waypoints */
public class ZonaSpawnEnemi1 : MonoBehaviour
{
    [Header("Configuración de la Zona")]
    [SerializeField] private Transform[] puntosSpawn; // A futuro
    [SerializeField] private Transform[] puntosPatrullaje; // Waypoints que usaran los enemigos en esta zona

    /******* GETTERS *******/

    /** Getter para obtener los puntos de spawn de la zona */
    public Transform[] GetPuntosSpawn()
    {
        return puntosSpawn;
    }

    /** Getter para obtener los puntos de patrullaje de la zona */
    public Transform[] GetPuntosPatrullaje()
    {
        return puntosPatrullaje;
    }
}
