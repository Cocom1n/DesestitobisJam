using UnityEngine;

/** Componente principal del jugador mediante el character controller */
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //private PlayerMovement movement;
    private MovimientoJugadorRecto movement;
    private PlayerJump jump;
    private PlayerRotation rotation;
    private IMover motor;
    private IPlayerInput input;
    private InteractorJugador interactor;

    private void Awake()
    {
        input = GetComponent<IPlayerInput>();
        motor = GetComponent<IMover>();
        interactor = GetComponent<InteractorJugador>();

        movement = GetComponent<MovimientoJugadorRecto>();
        jump = GetComponent<PlayerJump>();
        rotation = GetComponent<PlayerRotation>();
    }

    private void Update()
    {
        if (input == null || motor == null) return;

        /** 1. Gestion de Movimiento */
        Vector3 direccion =  movement.CalcularDireccion(input.EntradaMovimiento);
        float vertical = jump.CalcularVelocidadVertical(input.SaltoPresionado);

        Vector3 finalMove = direccion + Vector3.up * vertical;

        rotation.Rotar(direccion);
        motor.Mover(finalMove);

        /** 2. Gestion de Interaccion */
        if (interactor != null)
        {
            if (input.InteraccionPresionada)
            {
                interactor.IntentarRecolectar();
            }
            else if (input.SoltarMascaraPresionada)
            {
                interactor.SoltarPrioridad();
            }
        }
    }
}
