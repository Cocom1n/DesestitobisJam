using UnityEngine;

/** Implementacion de los stats del jugador y respuesta visual a efectos */
public class PlayerStats : MonoBehaviour, IEffectTarget
{
    [Header("Stats Base")]
    [SerializeField] private float topSpeed = 5;
    [SerializeField] private int lives = 3;
    [SerializeField] private float damage = 1;

    /** Referencias visuales para efectos como el entintado */
    private Renderer[] renderers;
    private Color originalColor = Color.white;

    private void Awake()
    {
        // Buscar todos los renderers para aplicar el color correctamente
        renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0)
        {
            // Se asume que el primer material tiene el color representativo
            originalColor = renderers[0].material.color;
        }
    }

    public void SetTopSpeed(float value) 
    { 
        topSpeed = value; 
    }

    public float GetTopSpeed()
    {
        return topSpeed;
    }

    public void SetLives(int value)
    {
        lives = value;
    }

    public int GetLives()
    {
        return lives;
    }

    public void SetDamage(float value)
    {
        damage = value;
    }

    public float GetDamage()
    {
        return damage;
    }

    /** Cambia el color de todos los renderers del jugador */
    public void SetColor(Color color)
    {
        if (renderers == null) return;

        foreach (var r in renderers)
        {
            if (r != null && r.material != null)
            {
                r.material.color = color;
            }
        }
    }

    /** Restablece el color al original */
    public void ResetColor()
    {
        SetColor(originalColor);
    }
}
