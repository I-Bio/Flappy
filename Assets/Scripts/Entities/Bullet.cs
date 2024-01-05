using System.Collections;
using Enums;
using Spawners;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IInteractable, IPoolObject
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody2D;
        private Vector2 _moveDirection;
        
        public BulletTarget Target { get; private set; }
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        public void Initialize(BulletTarget bulletTarget, Vector3 spawnPosition, Vector2 moveDirection)
        {
            Target = bulletTarget;
            transform.position = spawnPosition;
            _moveDirection = moveDirection;
            
            Rotate();
            gameObject.SetActive(true);
            StartCoroutine(MoveBullet());
        }
        
        public void Remove()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator MoveBullet()
        {
            while (true)
            {
                Move();
                yield return null;
            }
        }

        private void Move()
        {
            _rigidbody2D.velocity = _moveDirection * _speed;
        }

        private void Rotate()
        {
            float angleOfRotation = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg;

            if (angleOfRotation < 0) 
                angleOfRotation += 360;
        
            transform.eulerAngles = new Vector3(0, angleOfRotation, 0);
        }
    }
}
