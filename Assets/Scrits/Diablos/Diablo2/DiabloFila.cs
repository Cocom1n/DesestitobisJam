using UnityEngine;

public class DiabloFila : MonoBehaviour
{
    public float velocidad = 5f;
    public float distanciaDescenso = 1f;

    public float limiteInferior = -3f;
    public float limiteSuperior = 3f;
    
    private int direccionHorizontal = 1; // 1 = Derecha, -1 = Izquierda
    private int direccionVertical = -1;  // -1 baja, 1 sube

    void Update()
    {
        //transform.Translate(Vector3.right * direccionHorizontal * velocidad * Time.deltaTime);
        transform.Translate(Vector3.right * direccionHorizontal * velocidad * Time.deltaTime, Space.World);
        AjustarRotacion();
    }
    void AjustarRotacion()
    {
        if (direccionHorizontal == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pared"))
        {
            CambiarDeNivel();
        }
    }

    void CambiarDeNivel()
    {
        direccionHorizontal *= -1;
        transform.position += Vector3.forward * direccionVertical * distanciaDescenso;

        if (transform.position.z <= limiteInferior)
        {
            direccionVertical = 1; // Empezar a subir
        }
        else if (transform.position.z >= limiteSuperior)
        {
            direccionVertical = -1; // Empezar a bajar
        }
    }
}
