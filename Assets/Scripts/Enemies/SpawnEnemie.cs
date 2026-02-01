using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemie : MonoBehaviour
{
    public List<GameObject> prefabsEnemigos;
    public Transform[] puntosSpawn;

    public float tiempoEntreSpawns = 2f;
    public bool spawnActivo = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RutinaSpawn());
    }

    IEnumerator RutinaSpawn()
    {
        while (spawnActivo)
        {
            yield return new WaitForSeconds(tiempoEntreSpawns);
            
            IntentarSpawn();
        }
    }

    void IntentarSpawn()
    {
        if (prefabsEnemigos.Count == 0) return;

        GameObject candidato = prefabsEnemigos[Random.Range(0, prefabsEnemigos.Count)];

        int conteo = ContarEnemigosPorNombre(candidato.name);

        if (conteo < 5)
        {
            Transform puntoElegido = puntosSpawn[Random.Range(0, puntosSpawn.Length)];
            Instantiate(candidato, puntoElegido.position, puntoElegido.rotation);
        }
        else
        {
            //Debug.Log($"Límite alcanzado para {candidato.name}");
        }
    }

    int ContarEnemigosPorNombre(string nombrePrefab)
    {
        GameObject[] todosLosEnemigos = GameObject.FindGameObjectsWithTag("Enemy");
        int contador = 0;

        foreach (GameObject e in todosLosEnemigos)
        {
            if (e.name.Contains(nombrePrefab))
            {
                contador++;
            }
        }
        return contador;
    }
}
