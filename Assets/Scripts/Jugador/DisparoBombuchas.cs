using UnityEngine;

public class DisparoBombuchas : MonoBehaviour
{
    //[SerializeField] private GameObject bombucha;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private Camera camaraPrincipal;

    [SerializeField] private float fuerzaLanzamiento = 20f;
    [SerializeField] private float arcoVision = 180f;

    [SerializeField] private Bombucha[] poolDeBombuchas;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LanzarHaciaMouse();
        }
    }

    private void LanzarHaciaMouse()
    {
        Ray ray = camaraPrincipal.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 direccionHaciaMouse = hit.point - transform.position;
            direccionHaciaMouse.y = 0;

            float angulo = Vector3.SignedAngle(transform.forward, direccionHaciaMouse, Vector3.up);

            float limite = arcoVision / 2f;
            angulo = Mathf.Clamp(angulo, -limite, limite);

            Vector3 direccionFinal = Quaternion.Euler(0, angulo, 0) * transform.forward;

            /*GameObject bala = Instantiate(bombucha, puntoDisparo.position, Quaternion.identity);
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(direccionFinal * fuerzaLanzamiento, ForceMode.Impulse);
            }*/
            
            foreach (Bombucha b in poolDeBombuchas)
            {
                if (!b.gameObject.activeInHierarchy)
                {
                    b.Lanzar(puntoDisparo.position, direccionFinal * fuerzaLanzamiento);
                    break; 
                }
            }

            Debug.DrawRay(puntoDisparo.position, direccionFinal * 5, Color.red, 2f);
        }
    }
}
