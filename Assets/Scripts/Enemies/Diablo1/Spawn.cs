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

    [Header("Patrullaje")]

    private Coroutine spawnCoroutine;

    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (enemigosActivos < maxEnemigos) 
        {
            Transform spawnPoint = puntosSpawn[Random.Range(0, puntosSpawn.Length)];

            GameObject diablo = Instantiate(diabloPrefab, spawnPoint.position, Quaternion.identity);

            DiabloMover diabloMover = diablo.GetComponent<DiabloMover>();
            diabloMover.SetPuntosPatrulla(puntosSpawn); 
            diabloMover.InitPatrulla();

            enemigosActivos++;

            yield return new WaitForSeconds(2f);
        }
    }

    public void EliminarEnemigo()
    {
        enemigosActivos--;
    }
}
