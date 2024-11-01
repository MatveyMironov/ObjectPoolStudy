using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;
    private float _deathTime;

    private float _lifeTime;

    private BulletPool _bulletPool;

    public event Action OnBulletDisabled;

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

        CountLifeTime();
    }

    private void OnDisable()
    {
        OnBulletDisabled?.Invoke();
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
