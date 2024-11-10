using ObjectPoolSystem;
using PlayerSystem;
using ShootingSystem;
using UnityEngine;

namespace Core
{
    public class BootStrapper : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform bulletPoolRoot;

        [Space]
        [SerializeField] private InputListener inputListener;
        [SerializeField] private PlayerShooter playerShooter;

        private BulletPool _bulletPool;

        private void Start()
        {
            _bulletPool = new(bulletPrefab, bulletPoolRoot);

            inputListener.Construct(playerShooter);
            playerShooter.Construct(_bulletPool);
        }
    }
}
