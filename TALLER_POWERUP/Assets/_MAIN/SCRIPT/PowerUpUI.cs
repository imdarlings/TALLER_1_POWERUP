using UnityEngine;
using TMPro;

public class PowerUpUI : MonoBehaviour
{
    public PlayerStats playerStats;

    public TMP_InputField valueInput;
    public TMP_Text messageText;

    private PowerUpType selectedPowerUp;

    public enum PowerUpType
    {
        Heal,
        SpeedBoost,
        Shield,
        DamageBoost
    }

         // SELECCIÓN

    public void SelectHeal() => SelectPowerUp(PowerUpType.Heal);
    public void SelectSpeedBoost() => SelectPowerUp(PowerUpType.SpeedBoost);
    public void SelectShield() => SelectPowerUp(PowerUpType.Shield);
    public void SelectDamageBoost() => SelectPowerUp(PowerUpType.DamageBoost);

    private void SelectPowerUp(PowerUpType type)
    {
        selectedPowerUp = type;
        messageText.text = "Seleccionado: " + type.ToString();
    }

         // BOTÓN APLICAR

    public void ApplySelectedPowerUp()
    {
        if (!ValidateReferences()) return;

        if (!TryReadValue(out float value)) return;

        if (!ValidateRules(value)) return;

        ApplyPowerUp(value);
    }

         // VALIDACIONES

    private bool ValidateReferences()
    {
        if (playerStats == null || valueInput == null || messageText == null)
        {
            Debug.LogError("Faltan referencias en el Inspector.");
            return false;
        }

        return true;
    }

    private bool TryReadValue(out float value)
    {
        if (!float.TryParse(valueInput.text, out value))
        {
            messageText.text = "Valor inválido.";
            return false;
        }

        return true;
    }

    private bool ValidateRules(float value)
    {
        if (value <= 0)
        {
            messageText.text = "El valor debe ser mayor a 0.";
            return false;
        }

        switch (selectedPowerUp)
        {
            case PowerUpType.Heal:
                if (playerStats.currentHealth >= playerStats.maxHealth)
                {
                    messageText.text = "Vida ya está al máximo.";
                    return false;
                }
                break;

            case PowerUpType.Shield:
                if (playerStats.shieldActive)
                {
                    messageText.text = "El escudo ya está activo.";
                    return false;
                }
                break;
        }

        return true;
    }

        // SWITCH

    private void ApplyPowerUp(float value)
    {
        switch (selectedPowerUp)
        {
            case PowerUpType.Heal:
                playerStats.Heal(value);
                messageText.text = "Vida actual: " + playerStats.currentHealth;
                break;

            case PowerUpType.SpeedBoost:
                playerStats.SetSpeedMultiplier(value);
                messageText.text = "Velocidad actual: " + playerStats.currentSpeed;
                break;

            case PowerUpType.Shield:
                playerStats.SetShield(true);
                messageText.text = "Escudo activado.";
                break;

            case PowerUpType.DamageBoost:
                messageText.text = "Daño aumentado en: " + value;
                break;
        }
    }
}