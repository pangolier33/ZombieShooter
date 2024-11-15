using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _health;
    public delegate void HealthChanged(int currentHealth);
    public event HealthChanged OnHealthChanged;

    public int HealthValue
    {
        get { return _health; }
        set
        {
            _health = value;
            OnHealthChanged?.Invoke(_health);
        }
    }
}
