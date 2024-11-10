using UnityEngine;

public class BulletPool : GenericPool<Bullet>
{
    private Bullet _bulletPrefab;
    private Transform _root;

    public BulletPool(Bullet bulletPrefab, Transform root) : base(5, 5, 100)
    {
        _bulletPrefab = bulletPrefab;
        _root = root;

        CreatePool();
    }

    protected override Bullet CreateObject()
    {
        Bullet bulletInstance = Object.Instantiate(_bulletPrefab, _root);
        bulletInstance.gameObject.SetActive(false);
        return bulletInstance;
    }
}
