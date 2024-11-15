using UnityEngine;

public class PlayerHealthModify : MonoBehaviour
{
    [SerializeField] private HealthComponent PlayerHealth;
    
    private Animator _animator;
    
    private void OnEnable()
    {
        PlayerHealth.OnHealthChanged += DecreaseHealth;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void DecreaseHealth(int currenthealth)
    {
        if (currenthealth > 0)
        {
            TakeHit();
        }
        else
        {
            Die();
        }
    }
    
    private void TakeHit()
    {
        Debug.Log("Hit");
    }

    private void Die()
    {
        throw new System.NotImplementedException();
    }
    
    private void OnDisable()
    {
        PlayerHealth.OnHealthChanged -= DecreaseHealth;
    }
}
