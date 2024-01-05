using System;
using System.Collections.Generic;
using System.Linq;
using Attributes;
using UnityEngine;

namespace Spawners
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField, SerializeInterface(typeof(IPoolObject))] private GameObject _template;
        [SerializeField] private float _count;

        private List<GameObject> _pool;

        protected void Initialize()
        {
            _pool = new List<GameObject>();
            
            for (int i = 0; i < _count; i++)
            {
                GameObject spawned = Instantiate(_template, _container);
                
                spawned.SetActive(false);
                _pool.Add(spawned);
            }
        }
        
        protected void RemoveActiveObjects()
        {
            var activeObjects = _pool.Where(obj => obj.activeSelf == true).ToList();

            foreach (var activeObject in activeObjects)
            {
                activeObject.SetActive(false);
            }
        }

        protected void RemoveActiveObjectsWithComponent<T>(Action<T> action) where T: class
        {
            var activeObjects = _pool.Where(obj => obj.activeSelf == true).ToList();

            foreach (var activeObject in activeObjects)
            {
                if (activeObject.TryGetComponent(out T component) == false)
                    continue;

                action?.Invoke(component);
            }
        }

        protected bool TryGetFirstObject(out GameObject result)
        {
            result = _pool.FirstOrDefault(obj => obj.activeSelf == false);
            return result != null;
        }

        protected bool TryGetFirstObjectComponent<T>(out T result) where T : class
        {
            var available = _pool.FirstOrDefault(obj => obj.activeSelf == false);
            result = null;

            if (available != null && available.TryGetComponent(out T component) == true)
                result = component;

            return result != null;
        }
    }
}
