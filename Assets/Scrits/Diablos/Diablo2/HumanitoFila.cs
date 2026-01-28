using UnityEngine;
using System.Collections.Generic;
public class HumanitoFila : MonoBehaviour
{
    public Transform objetivoASeguir;
    public float distanciaMinima = 1.0f;
    public float velocidadSuave = 10f;

    private List<Vector3> historialPosiciones = new List<Vector3>();

    void Update()
    {
        if (objetivoASeguir != null)
        {
            historialPosiciones.Add(objetivoASeguir.position);

            if (Vector3.Distance(transform.position, objetivoASeguir.position) > distanciaMinima)
            {
                transform.position = Vector3.Lerp(transform.position, historialPosiciones[0], Time.deltaTime * velocidadSuave);
                
                historialPosiciones.RemoveAt(0);
            }
        }
    }
}
