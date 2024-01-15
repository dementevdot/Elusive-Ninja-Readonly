using System.Collections;
using UnityEngine;

namespace Game.Enemy
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private uint _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxFlightDuration;

        private Coroutine _shootCoroutine;
        private Vector3 _velocity;

        public static Vector3 PlayerHitVelocity { get; private set; }
        public uint Damage => _damage;

        public void Shoot(Vector3 direction)
        {
            if (_shootCoroutine != null)
                StopCoroutine(_shootCoroutine);

            _velocity = direction;

            _shootCoroutine = StartCoroutine(BulletFlight(direction));
        }

        public void Destroy()
        {
            if (_shootCoroutine != null)
                StopCoroutine(_shootCoroutine);

            Destroy(gameObject);
        }

        public void SetPlayerHitVelocity()
        {
            PlayerHitVelocity = _velocity;
        }

        private IEnumerator BulletFlight(Vector3 direction)
        {
            direction = direction.normalized;

            float elapsedTime = 0;

            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            while (elapsedTime < _maxFlightDuration)
            {
                transform.position += direction * (_speed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Destroy();
        }
    }
}