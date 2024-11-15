using System;
using UnityEngine;

namespace _Scripts.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 15f;
    
        public void Launch(Vector3 direction)
        {
            direction.Normalize();
            transform.up = direction;
            GetComponent<Rigidbody>().velocity = direction * speed;
        }
        private void OnEnable()
        {
            Invoke(nameof(Deactivate), 2f); 
        }

        private void Deactivate()
        {
            gameObject.SetActive(false); 
        }

        private void OnDisable()
        {
            CancelInvoke(); 
        }

        private void OnCollisionEnter(Collision other)
        {
            gameObject.SetActive(false);
        }
    }
}
