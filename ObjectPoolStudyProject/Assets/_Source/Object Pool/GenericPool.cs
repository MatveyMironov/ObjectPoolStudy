using System.Collections.Generic;

namespace ObjectPoolSystem
{
    public abstract class GenericPool<T> where T : IPoolable<T>
    {
        private Queue<T> _storedObjects;
        private List<T> _usedObjects;

        private readonly int _startPoolSize;
        private readonly int _defaultPoolExtand;
        private readonly int _maxPoolSize;

        private int _currentPoolSize { get { return _storedObjects.Count + _usedObjects.Count; } }

        public GenericPool(int startPoolSize, int defaultPoolSize, int maxPoolSize)
        {
            _startPoolSize = startPoolSize;
            _defaultPoolExtand = defaultPoolSize;
            _maxPoolSize = maxPoolSize;

            _storedObjects = new();
            _usedObjects = new();
        }

        protected void CreatePool()
        {
            AddObjects(_startPoolSize);
        }

        public void ClearPool()
        {
            foreach (T poolable in _storedObjects)
                poolable.OnObjectDisabled -= ReturnObject;

            _storedObjects.Clear();
        }

        public bool TryGetObject(out T poolable)
        {
            poolable = default;

            if (_storedObjects.Count > 0)
            {
                poolable = _storedObjects.Dequeue();
                _usedObjects.Add(poolable);

                return true;
            }
            else
                if (_currentPoolSize < _maxPoolSize)
            {
                ExtendPoolSize();

                poolable = _storedObjects.Dequeue();
                _usedObjects.Add(poolable);

                return true;
            }

            return false;
        }

        private void ReturnObject(T poolable)
        {
            if (_storedObjects.Contains(poolable)) return;
            _usedObjects.Remove(poolable);
            _storedObjects.Enqueue(poolable);
        }

        private void ExtendPoolSize()
        {
            int availablePoolExtand = _maxPoolSize - _currentPoolSize;

            if (availablePoolExtand >= _defaultPoolExtand)
                AddObjects(_defaultPoolExtand);
            else
                AddObjects(availablePoolExtand);
        }

        private void AddObjects(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                T instance = CreateObject();
                instance.OnObjectDisabled += ReturnObject;
                _storedObjects.Enqueue(instance);
            }
        }

        protected abstract T CreateObject();
    }
}