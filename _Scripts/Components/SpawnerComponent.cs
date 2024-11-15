using System.Collections;
using UnityEngine;

namespace _Scripts.Components
{
    public class SpawnerComponent : MonoBehaviour
    {
        [SerializeField] private float spawnDelay = 12f;
        [SerializeField] private float nextSpawnTime;
        [SerializeField] private Transform _prefab;
    
        private void Update()
        {
            if (ReadyToSpawn())
                StartCoroutine(Spawn());
        }

        private bool ReadyToSpawn() => Time.time >= nextSpawnTime;

        public IEnumerator Spawn()
        {
            nextSpawnTime = Time.time + spawnDelay;
            Instantiate(_prefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(3f);  
        }
    
    }
}
