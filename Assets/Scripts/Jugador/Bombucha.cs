using UnityEngine;

public class Bombucha : MonoBehaviour
{
    private Rigidbody rb;
    private void Awake() => rb = GetComponent<Rigidbody>();
    
    private void OnCollisionEnter(Collision collision)
    {
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
