using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private HealthComponent _playerHealth;
    [SerializeField] private TMP_Text _healthText;

    private void OnEnable()
    {
        _playerHealth.OnHealthChanged += UpdateHealthDisplay;
        _healthText.text = $"{_playerHealth.HealthValue * 20}%";
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= UpdateHealthDisplay;
    }
    
    private void UpdateHealthDisplay(int currentHealth)
    {
        _healthText.text = $"{currentHealth * 20}%";
    }
}
