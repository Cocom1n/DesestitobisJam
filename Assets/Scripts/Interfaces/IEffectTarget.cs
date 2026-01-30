using UnityEngine;

public interface IEffectTarget
{
    /** Velocidad maxima del objetivo */
    float GetTopSpeed();
    void SetTopSpeed(float value);

    /** Vida del objetivo */
    int GetLives();
    void SetLives(int value);

    /** Danio del objetivo */
    float GetDamage();
    void SetDamage(float value);

    /** Cambia el color visual del objetivo (entintado) */
    void SetColor(Color color);
}
