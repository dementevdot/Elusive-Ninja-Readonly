using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerMovment))]
    public class PlayerSlowMotion : MonoBehaviour
    {
        [SerializeField] private float _slowMotionSpeed = 0.25f;
        [SerializeField] private float _slowMotionSmooth = 1.5f;

        private PlayerMovment _movment;
        private PlayerHealth _health;
        private SlowMotionHandler _slowMotionHandler;

        public void Init(PlayerMovment movment, SlowMotionHandler slowMotionHandler, PlayerHealth health)
        {
            _movment = movment;
            _slowMotionHandler = slowMotionHandler;
            _health = health;

            _movment.OnJump += StartSlowMotion;
            _movment.OnGround += _slowMotionHandler.Stop;
            _health.Damaged += _slowMotionHandler.StopSmooth;
        }

        private void OnDisable()
        {
            _movment.OnJump -= StartSlowMotion;
            _movment.OnGround -= _slowMotionHandler.Stop;
            _health.Damaged -= _slowMotionHandler.StopSmooth;
        }

        private void StartSlowMotion() => _slowMotionHandler.SetStart(1f, _slowMotionSpeed, _slowMotionSmooth);
    }
}