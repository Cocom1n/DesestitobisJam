using UnityEngine;
using System.Collections.Generic;
public class HumanitoFila : MonoBehaviour
{
    public Transform objetivoASeguir;
    public float distanciaMinima = 0.3f;
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
    public void AscenderALider()
    {
        DiabloFila nuevoLider = gameObject.AddComponent<DiabloFila>();

        nuevoLider.velocidad = 5f;
        nuevoLider.distanciaDescenso = 1f;
        nuevoLider.limiteInferior = -3f;
        nuevoLider.limiteSuperior = 3f;

        GetDamage scriptDanio = GetComponent<GetDamage>();
        if (scriptDanio != null)
        {
            scriptDanio.enabled = true;
        }

        Destroy(this);
    }
}
