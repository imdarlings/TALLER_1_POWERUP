using UnityEngine;
using TMPro;

public class PowerUpUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private TMP_InputField valueInput;
    [SerializeField] private TMP_Text messageText;

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
                if (playerStats.CurrentHealth >= playerStats.MaxHealth)
                {
                    messageText.text = "Vida ya está al máximo: 100.";
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
                messageText.text = "Vida actual: " + playerStats.CurrentHealth;
                break;

            case PowerUpType.SpeedBoost:
                playerStats.SetSpeedMultiplier(value);
                messageText.text = "Velocidad actual: " + playerStats.CurrentSpeed;
                break;

            case PowerUpType.Shield:
                playerStats.ToggleShield();

                if (playerStats.ShieldActive)
                    messageText.text = "Escudo activado.";
                else
                    messageText.text = "Escudo desactivado.";

                break;

            case PowerUpType.DamageBoost:

                bool damaged = playerStats.TakeDamage(value);

                if (!damaged)
                {
                    messageText.text = "Estás protegido. Vida actual: " + playerStats.CurrentHealth;
                }
                else
                {
                    messageText.text = "Vida actual: " + playerStats.CurrentHealth;
                }

                break;
        }
    }
}