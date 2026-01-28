using UnityEngine;

public class DiabloRotacion : MonoBehaviour
{
    [SerializeField] private float suavizado = 5.5f;

    public void RotarHacia(Vector3 objetivo)
    {
        Vector3 _dir = objetivo - transform.position;
        _dir.y = 0f;

        if (_dir.sqrMagnitude < 0.0001f) return;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(_dir),
            suavizado * Time.deltaTime
        );
    }
}
