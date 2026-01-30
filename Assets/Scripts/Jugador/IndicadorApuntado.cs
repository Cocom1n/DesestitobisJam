using UnityEngine;

public class IndicadorApuntado : MonoBehaviour
{
    [SerializeField] private Transform flecha3D;
    [SerializeField] private Camera camaraPrincipal;

   [SerializeField] private float arcoVision = 180f;
    [SerializeField] private LayerMask capaSuelo;
    private DisparoBombuchas scriptDisparo;
    void Start()
    {
        scriptDisparo = GetComponent<DisparoBombuchas>();
    }
    void Update()
    {
        if (scriptDisparo != null && !scriptDisparo.PuedeDisparar)
        {
            flecha3D.gameObject.SetActive(false);
            return;
        }
        ActualizarRotacionIndicador();
    }

    private void ActualizarRotacionIndicador()
    {
        Ray ray = camaraPrincipal.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, capaSuelo))
        {
            flecha3D.gameObject.SetActive(true);

            Vector3 dirHaciaMouse = hit.point - transform.position;
            dirHaciaMouse.y = 0;

            float angulo = Vector3.SignedAngle(transform.forward, dirHaciaMouse, Vector3.up);
            
            float limite = arcoVision / 2f;
            angulo = Mathf.Clamp(angulo, -limite, limite);

            flecha3D.localRotation = Quaternion.Euler(0, angulo, 0);
        }
        else
        {
            flecha3D.gameObject.SetActive(false);
        }
    }
}
