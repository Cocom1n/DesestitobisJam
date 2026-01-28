using UnityEngine;
using System.Collections;

/** Maneja el ataque de soga instantanea usando un objeto 3D */
public class SogaAtaque : MonoBehaviour
{
    [Header("Configuracion de Soga Instantanea")]
    [SerializeField] private GameObject prefabSoga3D;
    [SerializeField] private float velocidadLanzamiento = 100f; /** Muy alta para efecto instantaneo */
    [SerializeField] private float tiempoVidaSoga = 0.3f;

    private GameObject instanciaSoga;
    private bool estaAtacando;

    private void Awake()
    {
        if (prefabSoga3D != null)
        {
            instanciaSoga = Instantiate(prefabSoga3D, transform);
            instanciaSoga.SetActive(false);
        }
    }

    /** Lanza la soga de forma casi instantanea hacia el punto de la mano */
    public void LanzarSoga(Vector3 origen, Transform objetivoMano, IAgarraObjetos poseedor)
    {
        if (estaAtacando || instanciaSoga == null) return;
        StartCoroutine(RutinaSogaInstantanea(origen, objetivoMano, poseedor));
    }

    private IEnumerator RutinaSogaInstantanea(Vector3 origen, Transform objetivoMano, IAgarraObjetos poseedor)
    {
        estaAtacando = true;
        instanciaSoga.SetActive(true);
        instanciaSoga.transform.position = origen;

        float _progreso = 0;

        while (_progreso < 1f)
        {
            if (objetivoMano == null) break;

            Vector3 _posicionMano = objetivoMano.position;
            Vector3 _direccion = (_posicionMano - origen).normalized;
            float _distanciaActual = Vector3.Distance(origen, _posicionMano);
            
            _progreso += Time.deltaTime * velocidadLanzamiento / Mathf.Max(_distanciaActual, 0.1f);

            /** Posicionar y estirar hacia la mano SIN ROTACION EXTRA */
            instanciaSoga.transform.position = Vector3.Lerp(origen, _posicionMano, _progreso);
            if (_direccion != Vector3.zero)
            {
                instanciaSoga.transform.rotation = Quaternion.LookRotation(_direccion);
            }

            Vector3 _escala = instanciaSoga.transform.localScale;
            _escala.z = _distanciaActual * _progreso; 
            instanciaSoga.transform.localScale = _escala;

            yield return null;
        }

        /** Impacto instantaneo */
        if (poseedor != null && poseedor.TieneObjeto)
        {
            poseedor.PerderObjeto();
        }

        yield return new WaitForSeconds(tiempoVidaSoga);
        
        instanciaSoga.SetActive(false);
        estaAtacando = false;
    }
}
