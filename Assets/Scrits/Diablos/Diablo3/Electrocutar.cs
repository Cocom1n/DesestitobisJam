using UnityEngine;

public class Electrocutar : MonoBehaviour
{
    [SerializeField] private float intervaloAtaque = 5f;
    private float timer = 0f;
    private bool puedeAtacar = false;

    void Start()
    {
        timer = intervaloAtaque;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = intervaloAtaque;
            puedeAtacar = true;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = intervaloAtaque;
            puedeAtacar = true;
        }
        bool auz = collision.gameObject.TryGetComponent<IElectrocutable>(out IElectrocutable electrocutable);
        if (auz && puedeAtacar)
        {
            electrocutable.SoltarHielos();
            puedeAtacar = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        bool auz = other.gameObject.TryGetComponent<IElectrocutable>(out IElectrocutable electrocutable);
        if (auz && puedeAtacar)
        {
            electrocutable.SoltarHielos();
            puedeAtacar = false;
        }
    }
}
