using UnityEngine;
using System.Collections;

/** Clase encargada de gestionar el spawn de enemigos */
public class Spawn : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    [SerializeField] private GameObject diabloPrefab;
    [SerializeField] private ZonaSpawnEnemi1[] zonas;

    [Header("Configuración de Enemigos")]
    [SerializeField] private int maxEnemigos = 3;
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
            // Zona aleatoria
            ZonaSpawnEnemi1 zona = zonas[Random.Range(0, zonas.Length)];
            // Seleccionamos un punto de spawn aleatorio de la zona seleccionada
            Transform spawnPoint = zona.GetPuntosSpawn()[Random.Range(0, zona.GetPuntosSpawn().Length)];

            GameObject nuevoDiablo = Instantiate(diabloPrefab, spawnPoint.position, Quaternion.identity);
            DiabloIA diabloIA = nuevoDiablo.GetComponent<DiabloIA>();

            if (diabloIA != null)
            {
                diabloIA.AsignarPuntosDePatrullaje(zona.GetPuntosPatrullaje());
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
