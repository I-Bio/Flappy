using System.Collections;
using Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class EnemySpawner : ObjectPool
    {
        [SerializeField] private float _delay;
        [SerializeField] private float _lowerBound;
        [SerializeField] private float _upperBound;
        
        private void Awake()
        {
            Initialize();
            StartCoroutine(SpawnEnemies());
        }

        public void Reset()
        {
            RemoveActiveObjectsWithComponent<Enemy>(RemoveEnemy);
        }

        private IEnumerator SpawnEnemies()
        {
            var wait = new WaitForSeconds(_delay);

            while (true)
            {
                Spawn();
                yield return wait;
            }
        }

        private void Spawn()
        {
            if (TryGetFirstObjectComponent(out Enemy enemy) == false) 
                return;
            
            float spawnPointY = Random.Range(_lowerBound, _upperBound);
            Vector3 spawnPoint = new Vector3(transform.position.x, spawnPointY, 0);
                
            enemy.Initialize(spawnPoint);
        }

        private void RemoveEnemy(Enemy enemy)
        {
            enemy.Remove();
        }
    }
}
