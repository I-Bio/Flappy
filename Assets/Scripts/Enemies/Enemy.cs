using System.Collections;
using Entities;
using Enums;
using Spawners;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Shooter))]
    [RequireComponent(typeof(CollisionDetector))]
    public class Enemy : MonoBehaviour, IInteractable, IPoolObject
    {
        private Shooter _shooter;
        private CollisionDetector _collisionDetector;

        private void Awake()
        {
            _shooter = GetComponent<Shooter>();
            _collisionDetector = GetComponent<CollisionDetector>();
        }

        private void OnEnable()
        {
            _collisionDetector.Detected += ProcessCollision;
        }

        private void OnDisable()
        {
            _collisionDetector.Detected -= ProcessCollision;
        }

        public void Initialize(Vector3 spawnPosition)
        {
            transform.position = spawnPosition;
            
            gameObject.SetActive(true);
            StartCoroutine(ShootBullets());
        }
        
        public void Remove()
        {
            _shooter.Reset();
            gameObject.SetActive(false);
        }

        private void ProcessCollision(IInteractable interactable)
        {
            if (interactable is Bullet == false)
                return;

            var bullet = (Bullet)interactable;
            
            if (bullet.Target != BulletTarget.Enemy)
                return;
            
            bullet.Remove();
            Remove();
        }

        private IEnumerator ShootBullets()
        {
            var wait = new WaitForSeconds(_shooter.Delay);
            
            while (true)
            {
                _shooter.Shoot();
                yield return wait;
            }
        }
    }
}
