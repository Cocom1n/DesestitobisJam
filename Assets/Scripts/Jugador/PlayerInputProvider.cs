using UnityEngine;
using UnityEngine.InputSystem;

/** Suministra los valores del Input System de Unity a traves de IPlayerInput */
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputProvider : MonoBehaviour, IPlayerInput
{
    private PlayerInput entradaUnity;
    private InputAction accionMover;
    private InputAction accionMirar;
    private InputAction accionSalto;
    private InputAction accionInteraccion;

    public Vector2 EntradaMovimiento => accionMover.ReadValue<Vector2>();
    public Vector2 EntradaMirar => accionMirar.ReadValue<Vector2>();
    public bool SaltoPresionado => accionSalto.WasPressedThisFrame();
    public bool InteraccionPresionada => accionInteraccion.WasPressedThisFrame();
    public bool SoltarMascaraPresionada => Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame;

    private void Awake()
    {
        entradaUnity = GetComponent<PlayerInput>();
        
        /** Obtener referencias a las acciones del asset por defecto */
        accionMover = entradaUnity.actions["Move"];
        accionMirar = entradaUnity.actions["Look"];
        accionSalto = entradaUnity.actions["Jump"];
        accionInteraccion = entradaUnity.actions["Interact"];
    }
}
