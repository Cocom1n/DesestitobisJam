using UnityEngine;

//SCRIT PARA EL JUGADOR

public class SerElectocutado : MonoBehaviour, IElectrocutable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoltarHielos()
    {
        Debug.Log("Pedrito me electocutaste :d");
    }
}
