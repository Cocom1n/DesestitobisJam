using UnityEngine;
using System.Collections;

/** Clase encargada de gestionar el spawn de enemigos */
public class Spawn : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject diabloPrefab;
    public Transform[] puntosSpawn;

    [Header("Configuración de Enemigos")]
    public int maxEnemigos = 3;
    private int enemigosActivos = 0;

    /******* METODOS *******/

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    /** Metodo que gestiona el spawn de enemigos en el escenario */
    private IEnumerator SpawnEnemies()
    {
        while (enemigosActivos < maxEnemigos)
        {
            Transform spawnPoint = puntosSpawn[Random.Range(0, puntosSpawn.Length)];
            GameObject nuevoDiablo = Instantiate(diabloPrefab, spawnPoint.position, Quaternion.identity);
            DiabloIA diabloIA = nuevoDiablo.GetComponent<DiabloIA>();

            if (diabloIA != null)
            {
                diabloIA.AsignarPuntosDePatrullaje(puntosSpawn);
                diabloIA.Reaparecer();  // SI NO ENTIENDES NO LO QUITES D": 
            }

            enemigosActivos++;
            yield return new WaitForSeconds(1f);
        }
    }

    /** Metodo para eliminar un enemigo, decrementando el contador de enemigos activos */
    public void EliminarEnemigo()
    {
        enemigosActivos--;
    }

    /******* GETTERS *******/

    /** Getter para obtener la cantidad maxima de enemigos */
    public int MaxEnemigos
    {
        get { return maxEnemigos; }
    }

    /******* SETTERS *******/

    /** Setter para modificar la cantidad maxima de enemigos */
    public void SetMaxEnemigos(int max)
    {
        maxEnemigos = max;
    }
}
