using UnityEngine;

namespace PlayerSystem
{
    public class InputListener : MonoBehaviour
    {
        private PlayerShooter _playerShooter;

        private void Update()
        {
            ListenShootInput();
        }

        public void Construct(PlayerShooter playerShooter)
        {
            _playerShooter = playerShooter;
        }

        private void ListenShootInput()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
                _playerShooter.Shoot();
        }
    }
}