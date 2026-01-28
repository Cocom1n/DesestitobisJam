using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float alturaSalto = 1.2f;
    [SerializeField] private float gravedad = -9.81f;

    private float velocidadVertical;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public float CalcularVelocidadVertical(bool saltoPresionado)
    {
        if (controller.isGrounded)
        {
            velocidadVertical = -controller.skinWidth;

            if (saltoPresionado)
            {
                velocidadVertical = Mathf.Sqrt(alturaSalto * -2f * gravedad);
            }
        }

        velocidadVertical += gravedad * Time.deltaTime;
        return velocidadVertical;
    }
}
