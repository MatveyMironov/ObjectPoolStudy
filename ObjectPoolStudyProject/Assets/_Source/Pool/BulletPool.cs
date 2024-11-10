using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    private Bullet _bulletPrefab;
    private Transform _root;

    private Queue<Bullet> _storedBullets;
    private List<Bullet> _usedBullets;

    private const int _startPoolSize = 5;
    private const int _defaultPoolExtand = 5;
    private const int _maxPoolSize = 100;

    private int _currentPoolSize { get { return _storedBullets.Count + _usedBullets.Count; } }

    public BulletPool(Bullet bulletPrefab, Transform root)
    {
        _bulletPrefab = bulletPrefab;
        _root = root;

        InitPool(_bulletPrefab, _root);
    }

    private void InitPool(Bullet bulletPrefab, Transform root)
    {
        _storedBullets = new();
        _usedBullets = new();

        AddPoolSize(_startPoolSize);
    }

    public void ClearPool()
    {
        foreach (Bullet bullet in _storedBullets)
        {
            bullet.OnBulletDisabled -= ReturnBullet;
        }

        _storedBullets.Clear();
    }

    public bool TryGetBullet(out Bullet bullet)
    {
        bullet = null;

        if (_storedBullets.Count > 0)
        {
            bullet = _storedBullets.Dequeue();
            _usedBullets.Add(bullet);

            return true;
        }
        else
        {
            if (_currentPoolSize < _maxPoolSize)
            {
                ExtendPoolSize();

                bullet = _storedBullets.Dequeue();
                _usedBullets.Add(bullet);

                return true;
            }
        }

        return false;
    }

    public void ReturnBullet(Bullet bullet)
    {
        if (_storedBullets.Contains(bullet)) { return; }

        _usedBullets.Remove(bullet);
        _storedBullets.Enqueue(bullet);
    }

    private void ExtendPoolSize()
    {
        int availablePoolExtand = _maxPoolSize - _currentPoolSize;

        if (availablePoolExtand >= _defaultPoolExtand)
        {
            AddPoolSize(_defaultPoolExtand);
        }
        else
        {
            AddPoolSize(availablePoolExtand);
        }
    }

    private void AddPoolSize(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Bullet bulletInstance = Object.Instantiate(_bulletPrefab, _root);
            bulletInstance.gameObject.SetActive(false);
            bulletInstance.OnBulletDisabled += ReturnBullet;
            _storedBullets.Enqueue(bulletInstance);
        }
    }
}
