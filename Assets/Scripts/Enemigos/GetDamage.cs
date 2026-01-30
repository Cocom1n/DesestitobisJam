using UnityEngine;

public class GetDamage : MonoBehaviour, IDaniable
{
    [SerializeField] private int vidaMaxima = 10;
    private int vidaActual = 10;
    //private bool estaMuerto = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vidaActual = vidaMaxima;
        //estaMuerto = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        if(vidaActual <= 0)
        {
            Morir();
        }
    }

    public void Morir()
    {
        HumanitoFila[] todosLosSeguidores = FindObjectsByType<HumanitoFila>(FindObjectsSortMode.None);
        foreach (HumanitoFila seguidor in todosLosSeguidores)
        {
            if (seguidor.objetivoASeguir == this.transform)
            {
                seguidor.AscenderALider();
                break; 
            }
        }
        //estaMuerto = true;
        Destroy(gameObject, 1f);
        Debug.Log("Me mori :c");
    }
}
