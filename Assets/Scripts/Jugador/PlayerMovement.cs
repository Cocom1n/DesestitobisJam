using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private Transform camara;

    public Vector3 CalcularDireccion(Vector2 input)
    {
        if (input.sqrMagnitude < 0.01f)
            return Vector3.zero;

        Transform refCam = camara != null ? camara : transform;

        Vector3 forward = refCam.forward;
        Vector3 right = refCam.right;
        forward.y = right.y = 0;

        return (forward.normalized * input.y +
                right.normalized * input.x).normalized * velocidad;
    }
}
