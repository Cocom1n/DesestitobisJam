using UnityEngine;

public class DiabloFila : MonoBehaviour
{
    public float velocidad = 5f;
    public float distanciaDescenso = 1f;

    public float limiteInferior = -3f;
    public float limiteSuperior = 3f;
    
    private int direccionZ = 1; // 1 = Derecha, -1 = Izquierda
    private int direccionX = 1;  // -1 baja, 1 sube

    void Update()
    {
        //transform.Translate(Vector3.right * direccionHorizontal * velocidad * Time.deltaTime);
        transform.Translate(Vector3.forward * direccionZ * velocidad * Time.deltaTime, Space.World);
        AjustarRotacion();
    }
    /*void AjustarRotacion()
    {
        if (direccionHorizontal == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }*/
    void AjustarRotacion()
    {
        Vector3 direccionDeMovimiento = new Vector3(0, 0, direccionZ);

        if (direccionDeMovimiento != Vector3.zero)
        {
            Quaternion rotacionDestino = Quaternion.LookRotation(direccionDeMovimiento);
            transform.rotation = rotacionDestino;
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
        direccionZ *= -1;
        transform.position += Vector3.right * direccionX * distanciaDescenso;

        if (transform.position.x <= limiteInferior)
        {
            direccionX = 1; // Empezar a subir
        }
        else if (transform.position.x >= limiteSuperior)
        {
            direccionX = -1; // Empezar a bajar
        }
    }
}
