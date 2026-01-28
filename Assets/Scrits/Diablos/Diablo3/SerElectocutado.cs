using UnityEngine;
using TMPro;

//SCRIT PARA EL JUGADOR

public class SerElectocutado : MonoBehaviour, IElectrocutable
{
    [SerializeField] private float cantHielos = 5;
    private float maxHielos = 5;
    [SerializeField] private TextMeshProUGUI textoHielos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHielos = cantHielos;
    }

    // Update is called once per frame
    void Update()
    {
        textoHielos.text = cantHielos.ToString("0");
    }

    public void SoltarHielos()
    {
        cantHielos -= 1;
        Debug.Log("Pedrito me electocutaste :d");
        if (cantHielos <= 0)
        {
            Morir();
            cantHielos = 0;
        }
    }

    public void Morir()
    {
        Debug.Log("perdio todos los hielos jaja q bobo");
        //Destroy(gameObject);
    }
}
