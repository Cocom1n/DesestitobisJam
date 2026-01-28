using UnityEngine;

/** Interfaz para el manejo de la entrada del jugador */
public interface IPlayerInput
{
    Vector2 EntradaMovimiento { get; }
    Vector2 EntradaMirar { get; }
    bool SaltoPresionado { get; }
    bool InteraccionPresionada { get; }
    bool SoltarMascaraPresionada { get; }
}
