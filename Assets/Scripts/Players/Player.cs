using System;
using Enemies;
using Entities;
using Enums;
using Spawners;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(CollisionDetector))]
    [RequireComponent(typeof(Shooter))]
    [RequireComponent(typeof(ScoreCounter))]
    public class Player : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private PlayerMover _playerMover;
        private CollisionDetector _collisionDetector;
        private Shooter _shooter;
        private ScoreCounter _scoreCounter;

        public event Action GameOver; 

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerMover = GetComponent<PlayerMover>();
            _collisionDetector = GetComponent<CollisionDetector>();
            _shooter = GetComponent<Shooter>();
            _scoreCounter = GetComponent<ScoreCounter>();

            _playerInput.Player.Move.performed += ctx => _playerMover.Move();
            _playerInput.Player.Shoot.performed += ctx => _shooter.Shoot();
        }

        private void OnEnable()
        {
            _playerInput.Enable();

            _collisionDetector.Detected += ProcessCollision;
        }

        private void OnDisable()
        {
            _playerInput.Enable();
            
            _collisionDetector.Detected -= ProcessCollision;
        }

        public void Reset()
        {
            _playerMover.Reset();
            _scoreCounter.Reset();
            _shooter.Reset();
        }

        private void ProcessCollision(IInteractable interactable)
        {
            switch (interactable)
            {
                case Ground:
                case Enemy: 
                    Die();
                    break;
                
                case Bullet bullet:
                    CheckBullet(bullet);
                    break;
                
                case ScoreZone:
                    _scoreCounter.Add();
                    break;
            }
        }

        private void CheckBullet(Bullet bullet)
        {
            if (bullet.Target != BulletTarget.Player)
                return;
            
            Die();
        }

        private void Die()
        {
            GameOver?.Invoke();
        }
    }
}
