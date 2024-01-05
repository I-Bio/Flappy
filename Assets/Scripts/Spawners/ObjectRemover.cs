using UnityEngine;

namespace Spawners
{
    public class ObjectRemover : MonoBehaviour
    {
        private void OnValidate()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IPoolObject poolObject) == false)
                return;

            poolObject.Remove();
        }
    }
}
