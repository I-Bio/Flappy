using UnityEngine;

namespace Players
{
    public class PlayerTracker : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private float _xOffset;
    
        private void Update()
        {
            Track();
        }

        private void Track()
        {
            var position = transform.position;
            position.x = _player.transform.position.x + _xOffset;
            transform.position = position;
        }
    }
}
