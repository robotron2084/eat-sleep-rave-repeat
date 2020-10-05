using System.Collections.Generic;
using UnityEngine;

namespace GameJamStarterKit
{
    public abstract class BasePool<T> : MonoBehaviour where T : Component
    {
        public int PoolSize = 32;

        private readonly Queue<T> _pool = new Queue<T>();

        /// <summary>
        /// Gets the next object in the pool.
        /// <remarks>Remember to call SetActive(true) on the gameObject.</remarks>
        /// </summary>
        /// <returns></returns>
        public T GetNext()
        {
            if (_pool.Count == 0)
            {
                RegeneratePool();
            }

            var value = _pool.Dequeue();
            _pool.Enqueue(value);
            return value;
        }

        public void RegeneratePool()
        {
            _pool.Clear();
            for (var i = 0; i < PoolSize; ++i)
            {
                AddPoolItem();
            }
        }

        private void AddPoolItem()
        {
            var o = new GameObject("[Pool] Pool Item (" + (_pool.Count + 1) + ")");
            o.transform.SetParent(transform, false);
            var component = (T) o.AddComponent(typeof(T));
            InitializeComponent(component);
            _pool.Enqueue(component);
            o.SetActive(false);
        }

        protected abstract void InitializeComponent(T component);
    }
}