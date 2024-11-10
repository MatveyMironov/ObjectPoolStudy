using System;

namespace ObjectPoolSystem
{
    public interface IPoolable<T>
    {
        public event Action<T> OnObjectDisabled;
    }
}