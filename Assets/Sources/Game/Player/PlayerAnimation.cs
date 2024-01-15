using System;
using System.Collections;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerSkinView))]
    [RequireComponent(typeof(PlayerCombat))]
    [RequireComponent(typeof(PlayerMovment))]
    public class PlayerAnimation : MonoBehaviour
    {
        private readonly int Jump = Animator.StringToHash("OnJump");
        private readonly int Punch = Animator.StringToHash("OnPunch");
        private readonly int Idle = Animator.StringToHash("IsOnGround");
        private readonly int Running = Animator.StringToHash("IsRunning");

        [SerializeField][Range(0, 360)] private int _turningAngle;
        [SerializeField] private Transform _skinTransform;

        private Animator _animator;
        private PlayerMovment _movment;
        private PlayerSkinView _skinView;
        private PlayerCombat _combat;
        private PlayerHealth _health;
        private Coroutine _rotationCoroutine;

        public void Init(PlayerMovment movment, PlayerSkinView skinView, PlayerCombat combat, PlayerHealth health)
        {
            _movment = movment;
            _skinView = skinView;
            _combat = combat;
            _health = health;

            SetAnimator();

            _skinView.SkinChanged += SetAnimator;
            _movment.OnJump += StartJumpAnimation;
            _movment.OnGround += OnGround;
            _combat.OnPunch += SetPunchAnimation;
            _health.HealthIsZero += OnPlayerDied;
        }

        public void Rotate(int angle, float duration)
        {
            if (_rotationCoroutine != null)
                StopCoroutine(_rotationCoroutine);

            _rotationCoroutine = StartCoroutine(Rotation(angle, duration));
        }

        public void SetRunningAnimation(bool isStart) => _animator.SetBool(Running, isStart);

        private void SetJumpAnimation()
        {
            _animator.SetTrigger(Jump); 
            _animator.SetBool(Idle, false);
        }

        private void SetPunchAnimation() => _animator.SetTrigger(Punch);

        private void SetIdleAnimation(bool onGround) => _animator.SetBool(Idle, onGround);

        private void OnPlayerDied()
        {
            if (_rotationCoroutine != null)
                StopCoroutine(_rotationCoroutine);
        }

        private void OnDisable()
        {
            _skinView.SkinChanged -= SetAnimator;
            _movment.OnJump -= StartJumpAnimation;
            _movment.OnGround -= OnGround;
            _combat.OnPunch -= SetPunchAnimation;
            _health.HealthIsZero -= OnPlayerDied;

            if (_rotationCoroutine != null)
                StopCoroutine(_rotationCoroutine);
        }

        private void StartJumpAnimation()
        {
            int angle;

            if (_movment.Direction.x > 0)
                angle = -_turningAngle;
            else
                angle = _turningAngle;

            SetJumpAnimation();

            Rotate(angle, 0.25f);
        }

        private void SetAnimator()
        {
            _skinView.SkinChanged -= SetAnimator;
            _animator = _skinView.Animator;
        }

        private void OnGround() => SetIdleAnimation(true);

        private IEnumerator Rotation(int angle, float duration)
        {
            float elapsedTime = 0;
            var rotation = Quaternion.Euler(0, angle, 0);

            while (elapsedTime < 1)
            {
                elapsedTime += Time.deltaTime / duration;
                _skinTransform.rotation = Quaternion.Lerp(_skinTransform.rotation, rotation, elapsedTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
