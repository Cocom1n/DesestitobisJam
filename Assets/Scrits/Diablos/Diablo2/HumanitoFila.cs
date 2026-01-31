using UnityEngine;
using System.Collections.Generic;
public class HumanitoFila : MonoBehaviour
{
    public Transform objetivoASeguir;
    public float distanciaMinima = 0.3f;
    public float velocidadSuave = 10f;

    private List<Vector3> historialPosiciones = new List<Vector3>();
    public GameObject modeloHumano; 
    public GameObject modeloDiablo;

    void Update()
    {
        if (objetivoASeguir != null)
        {
            historialPosiciones.Add(objetivoASeguir.position);

            /*if (Vector3.Distance(transform.position, objetivoASeguir.position) > distanciaMinima)
            {
                Vector3 puntoDestino = historialPosiciones[0];

                Vector3 direccion = (puntoDestino - transform.position).normalized;
                if(direccion != Vector3.zero)
                {
                    Quaternion rotacionDestino = Quaternion.LookRotation(direccion);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDestino, Time.deltaTime * velocidadSuave);
                }

                //transform.position = Vector3.Lerp(transform.position, historialPosiciones[0], Time.deltaTime * velocidadSuave);
                transform.position = Vector3.Lerp(transform.position, puntoDestino, Time.deltaTime * velocidadSuave);
                historialPosiciones.RemoveAt(0);
            }*/

            if(historialPosiciones.Count >20)
            {
                Vector3 puntoDestino = historialPosiciones[0];

                transform.position = Vector3.Lerp(transform.position, puntoDestino, Time.deltaTime * velocidadSuave);

                Vector3 direccion = (puntoDestino - transform.position).normalized;
                if(direccion != Vector3.zero)
                {
                    Quaternion rotacionDestino = Quaternion.LookRotation(direccion);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDestino, Time.deltaTime * velocidadSuave);
                }
                historialPosiciones.RemoveAt(0);
            }
        }
    }
    public void AscenderALider()
    {
        if (modeloHumano != null && modeloDiablo != null)
        {
            modeloHumano.SetActive(false);
            modeloDiablo.SetActive(true); 
        }


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

        Electrocutar scriptElec = GetComponent<Electrocutar>();
        if(scriptElec != null)
        {
            scriptElec.enabled = true;
        }

        Destroy(this);
    }
}
