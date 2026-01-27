using UnityEngine;

/** Componente principal para el control del jugador usando CharacterController */
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerJump jump;
    private PlayerRotation rotation;
    private IMover motor;
    private IPlayerInput input;

    private void Awake()
    {
        input = GetComponent<IPlayerInput>();
        motor = GetComponent<IMover>();

        movement = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        rotation = GetComponent<PlayerRotation>();
    }

    private void Update()
    {
        if (input == null || motor == null) return;

        Vector3 dir = movement.CalcularDireccion(input.EntradaMovimiento);
        float vertical = jump.CalcularVelocidadVertical(input.SaltoPresionado);

        Vector3 finalMove = dir + Vector3.up * vertical;

        rotation.Rotar(dir);
        motor.Mover(finalMove);
    }
}
