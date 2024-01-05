using System;
using UnityEngine;

namespace Entities
{
    public class CollisionDetector : MonoBehaviour
    {
        public event Action<IInteractable> Detected;

        private void OnValidate()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                Detected?.Invoke(interactable);
            }
        }
    }
}