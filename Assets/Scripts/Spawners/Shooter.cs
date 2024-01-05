using System.Collections;
using Entities;
using Enums;
using UnityEngine;

namespace Spawners
{
    public class Shooter : ObjectPool
    {
        [SerializeField] private BulletTarget _bulletTarget;
        [SerializeField] private AttackSide _attackSide;
        [SerializeField] private float _delay;

        private bool _canAttack;

        public float Delay => _delay;
        
        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            _canAttack = true;
        }

        public void Reset()
        {
            RemoveActiveObjects();
        }

        public void Shoot()
        {
            if (_canAttack == false)
                return;
            
            if (TryGetFirstObjectComponent(out Bullet bullet) == false) 
                return;
        
            _canAttack = false;
            Vector2 moveDirection = _attackSide == AttackSide.Left ? Vector2.left : Vector2.right;
            
            bullet.Initialize(_bulletTarget, transform.position, moveDirection);
            StartCoroutine(DropCoolDown());
        }

        private IEnumerator DropCoolDown()
        {
            var wait = new WaitForSeconds(_delay);
            yield return wait;
            _canAttack = true;
        }
    }
}
