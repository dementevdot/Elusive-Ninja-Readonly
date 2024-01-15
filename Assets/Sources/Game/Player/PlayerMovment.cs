using System;
using System.Collections;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMovment : MonoBehaviour
    {
        private const int WallsLayer = 3;

        [SerializeField] private float _power = 10f;
        [SerializeField] private float _runSpeed = 1;
        [SerializeField] private ColliderRouter _colliderRouter;

        private PlayerAnimation _playerAnimation;
        private PlayerHealth _playerHealth;

        private Coroutine _onGroundCoroutine;
        private Coroutine _jumpVelocityCoroutine;

        public event Action OnGround;
        public event Action OnJump;

        public float Power => _power;
        public float RunSpeed => _runSpeed;
        public float VelocityToReach => 0;

        public Transform Transform { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public Vector2 Direction { get; private set; }
        private bool IsOnGround { get; set; } = true;

        private void OnDestroy()
        {
            _playerHealth.HealthIsZero -= TurnOffRigidbody;
        }

        public static float GetVelocityMultiplier(float velocity) => velocity * velocity;

        public void Init(PlayerAnimation playerAnimation, PlayerHealth health, Transform transform, Rigidbody rigidbody)
        {
            _playerAnimation = playerAnimation;
            Transform = transform;
            Rigidbody = rigidbody;
            _playerHealth = health; 

            _playerHealth.HealthIsZero += TurnOffRigidbody;
            _colliderRouter.CollisionEnter += ColliderRouterOnCollisionEnter;
        }

        public void SetJump(Vector2 direction)
        {
            Rigidbody.velocity = direction * _power;
            IsOnGround = false;

            if (_onGroundCoroutine != null)
                StopCoroutine(_onGroundCoroutine);

            if (_jumpVelocityCoroutine != null)
                StopCoroutine(_jumpVelocityCoroutine);

            _jumpVelocityCoroutine = StartCoroutine(JumpVelocityHandler());

            Direction = direction;
            OnJump?.Invoke();
        }

        public IEnumerator Run(Vector3 start, Vector3 end, float runSpeed, Action callback)
        {
            float elapsedTime = 0;
            int rotationWhileRun = -90;

            _playerAnimation.SetRunningAnimation(true);
            _playerAnimation.Rotate(rotationWhileRun, 0.1f);

            float distance = Vector3.Distance(start, end);

            while (elapsedTime < 1)
            {
                Transform.position = Vector3.Lerp(start, end, elapsedTime);
                elapsedTime += Time.deltaTime * runSpeed / distance;

                yield return new WaitForEndOfFrame();
            }

            _playerAnimation.SetRunningAnimation(false);

            callback?.Invoke();
        }

        public void SetPlayerPosition(Vector3 position) => Transform.position = position;

        public void TurnOffRigidbody()
        {
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.useGravity = false;
        }

        public void TurnOnRigidbody() => Rigidbody.useGravity = true;

        private void ColliderRouterOnCollisionEnter(Collision collision)
        {
            if (IsOnGround == true) return;
            if (collision.gameObject.layer != WallsLayer) return;
            if (collision.GetContact(0).normal != Vector3.up) return;

            OnGround?.Invoke();
            IsOnGround = true;
        }

        private IEnumerator JumpVelocityHandler()
        {
            float elapsedTime = 0;

            while (Rigidbody.velocity.y > VelocityToReach)
            {
                yield return new WaitForEndOfFrame();

                elapsedTime += Time.deltaTime * 0.3f;

                var velocity = Rigidbody.velocity;

                Rigidbody.velocity = new Vector3(
                    velocity.x,
                    Mathf.Lerp(velocity.y, VelocityToReach, GetVelocityMultiplier(elapsedTime)),
                    Rigidbody.velocity.z);
            }
        }
    }
}