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
        if (!this.enabled) return;
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

        Animator anim = GetComponentInChildren<Animator>();

        if(anim != null)
        {
            anim.SetTrigger("Morir");
        }

        if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = false;
        if (GetComponent<DiabloFila>() != null) GetComponent<DiabloFila>().enabled = false;
        if (GetComponent<HumanitoFila>() != null) GetComponent<HumanitoFila>().enabled = false;
        
        if (GetComponent<MovimientoAleatorio>() != null) GetComponent<MovimientoAleatorio>().enabled = false;

        if (GetComponent<DiabloMover>() != null) GetComponent<DiabloMover>().enabled = false;
        if (GetComponent<DiabloIA>() != null) GetComponent<DiabloIA>().enabled = false;

        //estaMuerto = true;
        Destroy(gameObject, 3f);
        Debug.Log("Me mori :c");
    }
}
