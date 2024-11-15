using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Weapon
{
    public class PlayerWeapon : MonoBehaviour
    {
        private const int PROJECTILE_PRELOAD_COUNT = 20;
        
        [SerializeField] private float _delay = 0.25f;
        [SerializeField] private LayerMask _aimLayerMask;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private Bullet _bulletPrefab;
        
        private PoolBase<Bullet> _bulletPool;

        private readonly CancellationTokenSource _source = new();
        
        private float _nextFireTime;
        private List<PowerUp> _powerUps = new List<PowerUp>();
    
        private void Awake()
        {
            _bulletPool = new PoolBase<Bullet>(
                Preload, 
                GetAction,
                ReturnAction, 
                PROJECTILE_PRELOAD_COUNT);
        }
        
        private void Update()
        {
            AimTowardMouse();
        
            if (ReadyToShoot())
                Shoot();
        }

        private void AimTowardMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _aimLayerMask))
            {
                Vector3 destination = hitInfo.point;
                destination.y = transform.position.y;

                Vector3 direction = destination - transform.position;
                direction.Normalize();
                transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
        }
        
        private void ReturnAllBullets() =>  _bulletPool.ReturnAll();

        private Bullet Preload() => Instantiate(_bulletPrefab);

        private void GetAction(Bullet bullet) => bullet.gameObject.SetActive(true);

        private void ReturnAction(Bullet bullet) => bullet.gameObject.SetActive(false);

        private void OnDestroy()
        {
            _source.Cancel();
            _source.Dispose();
        }

        private bool ReadyToShoot() => Time.time >= _nextFireTime;
    
        private void Shoot()
        {
            ShootDelay();
            
            Bullet bullet = _bulletPool.Get();
            bullet.transform.position = _firePoint.position;
            bullet.Launch(transform.forward);
        }

        private void ShootDelay()
        {
            float delay = _delay;
            foreach (var powerUp in _powerUps)
            {
                delay *= powerUp.DelayMultiplier;
            }
            _nextFireTime = Time.time + delay;
        }

        public void AddPowerUp(PowerUp powerUp) => _powerUps.Add(powerUp);

        public void RemovePowerUp(PowerUp powerUp) => _powerUps.Remove(powerUp);
    }
}
