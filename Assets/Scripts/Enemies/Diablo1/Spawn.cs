using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject diabloPrefab;
    public Transform[] puntosSpawn;

    [Header("Configuración de Enemigos")]
    public int maxEnemigos = 3;
    private int enemigosActivos = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (enemigosActivos < maxEnemigos)
        {
            // Elige una posición aleatoria de los puntos de spawn disponibles
            Transform spawnPoint = puntosSpawn[Random.Range(0, puntosSpawn.Length)];

            // Instanciamos un nuevo Diablo
            GameObject nuevoDiablo = Instantiate(diabloPrefab, spawnPoint.position, Quaternion.identity);

            // Obtenemos el script DiabloIA para poder configurarlo
            DiabloIA diabloIA = nuevoDiablo.GetComponent<DiabloIA>();

            if (diabloIA != null)
            {
                // Asignar los puntos de patrullaje
                diabloIA.AsignarPuntosDePatrullaje(puntosSpawn);

                // Cambiar el estado a Reaparición
                diabloIA.Reaparecer();
                Debug.Log("Puntos de patrullaje asignados y Diablo en estado de reaparición.");
            }

            enemigosActivos++;
            yield return new WaitForSeconds(1f);
        }
    }

    public void EliminarEnemigo()
    {
        enemigosActivos--;
    }
}
