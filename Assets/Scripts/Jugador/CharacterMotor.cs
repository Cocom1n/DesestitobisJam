using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMotor : MonoBehaviour, IMover
{
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Mover(Vector3 movimiento)
    {
        controller.Move(movimiento * Time.deltaTime);
    }
}
