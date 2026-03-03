using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public float baseSpeed = 5f;
    public float currentSpeed = 5f;

    public bool shieldActive = false;

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        currentSpeed = baseSpeed * multiplier;
    }

    public void SetShield(bool active)
    {
        shieldActive = active;
    }
}
