using UnityEngine;

namespace Entities
{
    public class ScoreZone : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _spawnPoint;

        private Vector3 _startPosition;
        
        private void Start()
        {
            SetPosition();
            _startPosition = transform.position;
        }

        public void Reset()
        {
            transform.position = _startPosition;
        }

        public void SetPosition()
        {
            float spawnPositionX = _spawnPoint.position.x;
            float spawnPositionY = transform.position.y;
            transform.position = new Vector3(spawnPositionX, spawnPositionY, 0f);
        }
    }
}
