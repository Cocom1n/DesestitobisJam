using UnityEngine;

public class Bombucha : MonoBehaviour
{
    private Rigidbody rb;
    private void Awake() => rb = GetComponent<Rigidbody>();
    [SerializeField] private int danio = 5;
    [SerializeField] private AudioClip sonidoExplosion;

    private void OnCollisionEnter(Collision collision)
    {
        IDaniable daniable = collision.transform.GetComponent<IDaniable>();
        if(daniable != null)
        {
            daniable.RecibirDanio(danio);
            AudioSource.PlayClipAtPoint(sonidoExplosion, transform.position);

        }
        Desactivar();
    }
    public void Lanzar(Vector3 posicion, Vector3 fuerza)
    {
        transform.position = posicion;
        gameObject.SetActive(true);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(fuerza, ForceMode.Impulse);
        
        Invoke(nameof(Desactivar), 2f);
    }
    private void Desactivar()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }
}
