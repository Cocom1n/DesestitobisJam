using UnityEngine;
using System.Collections.Generic;
public class HumanitoFila : MonoBehaviour
{
    public Transform objetivoASeguir; // El segmento justo delante de este
    public float distanciaMinima = 1.0f; // Espacio entre segmentos
    public float velocidadSuave = 10f;

    // Lista para guardar el rastro de posiciones del objetivo
    private List<Vector3> historialPosiciones = new List<Vector3>();

    void Update()
    {
        if (objetivoASeguir != null)
        {
            // 1. Registrar la posición actual del objetivo
            historialPosiciones.Add(objetivoASeguir.position);

            // 2. Si el objetivo se ha movido lo suficiente, empezamos a seguir
            if (Vector3.Distance(transform.position, objetivoASeguir.position) > distanciaMinima)
            {
                // Tomamos la posición más antigua del rastro
                transform.position = Vector3.Lerp(transform.position, historialPosiciones[0], Time.deltaTime * velocidadSuave);
                
                // Borramos la posición que ya alcanzamos para no acumular basura
                historialPosiciones.RemoveAt(0);
            }
        }
    }
}
