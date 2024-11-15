using System;
using _Scripts.Components;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Scripts.Creatures
{
    public class Zombie : MonoBehaviour
    {
        public static event Action Died;
        
        public float _attackRange = 1f;
    
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private AnimatorStateInfo _animatorState;
        private PlayerMovement player;
        private bool _isAttacking = false;
        private int _currentHealth;

        public Zombie(float attackRange)
        {
            _attackRange = attackRange;
        }
        
        private bool Alive => _currentHealth > 0;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }
        
        
        private void Update()
        {
            player = FindObjectOfType<PlayerMovement>();
            _currentHealth = GetComponent<HealthComponent>().HealthValue;
        
            if (!(_isAttacking == false || Alive))
                return;
        
            if (_navMeshAgent.enabled)
                _navMeshAgent.SetDestination(player.position);

            if (Vector3.Distance(transform.position, player.transform.position) < _attackRange)
                Attack();
        }
    
        private void Attack()
        {
            _isAttacking = true;
            _navMeshAgent.enabled = false;
            _animator.SetTrigger("Attack");
        }
    
        // Animation Callback
        private void AttackComplete()
        {
            if (Alive)
            {
                _isAttacking = false;
                _navMeshAgent.enabled = true;
            }
        }

        // Animation Callback
        private void AttackHit()
        {
            DamageComponent.Apply(player.gameObject);
        }

        public void Die()
        {
            _animator.SetTrigger("Died");
            gameObject.GetComponent<Collider>().enabled = false;
            _navMeshAgent.enabled = false;
            Died?.Invoke();
            Destroy(gameObject, 5f);
        }

        public void TakeHit()
        {
            if (_currentHealth <= 0)
            {
                Die();
                return;
            }
            
            _navMeshAgent.enabled = false;
            _animator.SetTrigger("Hit");
        }

        // Animation Callback
        private void HitComplete()
        {
            if (Alive)
                _navMeshAgent.enabled = true;
        }
        
    }
}
