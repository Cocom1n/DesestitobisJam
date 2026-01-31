using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject diabloPrefab; // Prefab del Diablo
    public Transform[] puntosSpawn; // Puntos de spawn donde los enemigos aparecerán

    [Header("Configuración de Enemigos")]
    public int maxEnemigos = 3; // Máximo de enemigos por nivel
    private int enemigosActivos = 0; // Número actual de enemigos activos

    [Header("Patrullaje")]

    private Coroutine spawnCoroutine; // La corutina que va a controlar el spawn

    // Iniciar el spawn de enemigos
    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    // Corutina para spawn de enemigos
    private IEnumerator SpawnEnemies()
    {
        while (enemigosActivos < maxEnemigos) // Mientras no se alcance el límite de enemigos
        {
            // Elegir un punto aleatorio para spawn
            Transform spawnPoint = puntosSpawn[Random.Range(0, puntosSpawn.Length)];

            // Instanciar Diablo
            GameObject diablo = Instantiate(diabloPrefab, spawnPoint.position, Quaternion.identity);

            // Asignar puntos de patrullaje al nuevo enemigo
            DiabloMover diabloMover = diablo.GetComponent<DiabloMover>();
            diabloMover.SetPuntosPatrulla(puntosSpawn); // Asignar puntos de patrullaje al enemigo
            diabloMover.InitPatrulla();

            // Aumentamos la cantidad de enemigos activos
            enemigosActivos++;

            // Esperar 2 segundos antes de intentar crear otro enemigo
            yield return new WaitForSeconds(2f);
        }
    }

    // Si el enemigo muere o es destruido, decrementamos la cantidad de enemigos activos
    public void EliminarEnemigo()
    {
        enemigosActivos--;
    }
}
