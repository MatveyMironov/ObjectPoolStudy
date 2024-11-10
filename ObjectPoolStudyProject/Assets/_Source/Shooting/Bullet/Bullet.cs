using ObjectPoolSystem;
using System;
using UnityEngine;

namespace ShootingSystem
{
    public class Bullet : MonoBehaviour, IPoolable<Bullet>
    {
        private float _speed;
        private float _deathTime;

        private float _lifeTime;

        public event Action<Bullet> OnObjectDisabled;

        private void Update()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;

            CountLifeTime();
        }

        private void OnDisable()
        {
            OnObjectDisabled?.Invoke(this);
        }

        public void SetupBullet(float speed, float deathTime)
        {
            _speed = speed;
            _deathTime = deathTime;
        }

        private void CountLifeTime()
        {
            _lifeTime += Time.deltaTime;

            if (_lifeTime >= _deathTime)
            {
                _lifeTime = 0;
                gameObject.SetActive(false);
            }
        }
    }
}