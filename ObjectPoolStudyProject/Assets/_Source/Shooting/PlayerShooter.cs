using ObjectPoolSystem;
using ShootingSystem;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Transform muzzle;

        [Header("Bullet")]
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletDeathTime;

        private BulletPool _bulletPool;

        public void Construct(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public void Shoot()
        {
            if (_bulletPool.TryGetObject(out Bullet bullet))
            {
                bullet.transform.position = muzzle.position;
                bullet.transform.rotation = muzzle.rotation;

                bullet.SetupBullet(bulletSpeed, bulletDeathTime);

                bullet.gameObject.SetActive(true);
            }
        }
    }
}