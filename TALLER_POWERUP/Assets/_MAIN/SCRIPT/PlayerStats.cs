using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;

    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float currentSpeed = 5f;

    [SerializeField] private bool shieldActive = false;

  
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public float CurrentSpeed => currentSpeed;
    public bool ShieldActive => shieldActive;



    public void Heal(float amount)
    {
        if (currentHealth >= maxHealth)
            return;

        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public bool TakeDamage(float amount)
    {
        if (shieldActive)
            return false; // No recibe daño

        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        return true; // Si recibe daño
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        currentSpeed = baseSpeed * multiplier;
    }

    public void ToggleShield()
    {
        shieldActive = !shieldActive;
    }
}