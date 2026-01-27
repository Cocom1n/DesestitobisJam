using UnityEngine;

public class Electrocutar : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        bool auz = collision.gameObject.TryGetComponent<IElectrocutable>(out IElectrocutable electrocutable);
        if (auz)
        {
            electrocutable.SoltarHielos();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
