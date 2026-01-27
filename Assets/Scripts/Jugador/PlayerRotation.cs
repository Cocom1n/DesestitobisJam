using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float velocidadRotacion = 10f;

    public void Rotar(Vector3 direccion)
    {
        if (direccion.sqrMagnitude < 0.001f) return;

        Quaternion objetivo = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            objetivo,
            velocidadRotacion * Time.deltaTime
        );
    }
}
