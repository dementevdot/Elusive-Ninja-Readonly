using System;
using System.Collections;
using Game.Player;
using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyCombat : MonoBehaviour
    {
        public const float MaxCombatTimeOut = 2;
        private const float MaxShootDistance = 10f;
        private const float RaycastTimeOut = 0.2f;

        [SerializeField] private CombatCollider _damageCollider;
        [SerializeField] private Bullet _bullet;

        private Enemy _enemy;
        private Coroutine _combatCoroutine;

        public event Action OnShoot;

        public float CurrentCombatTimeOut { get; private set; }

        public CombatCollider DamageCollider => _damageCollider;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void OnEnable()
        {
            _damageCollider.OnDamage += DamageHandler;

            if (_enemy.Died == false)
                _combatCoroutine = StartCoroutine(Combat());
        }

        private void OnDisable()
        {
            _damageCollider.OnDamage -= DamageHandler;
        }

        private void DamageHandler(uint damage, GameObject sender)
        {
            if (sender.TryGetComponent<PlayerCombat>(out var player) == false) return;

            StopCoroutine(_combatCoroutine);
            _enemy.TakeDamage(damage, sender);
        }

        private IEnumerator Combat()
        {
            WaitForSeconds raycastTimeOut = new WaitForSeconds(RaycastTimeOut);
            bool raycastReached = false;

            const float playerHeadHeight = 0.5f;
            const int playerLayer = 7;

            while (Root.PlayerMovment == null) yield return new WaitForFixedUpdate();

            PlayerMovment playerMovment = Root.PlayerMovment;

            while (_enemy.Died == false)
            {
                while (CurrentCombatTimeOut < MaxCombatTimeOut)
                {
                    CurrentCombatTimeOut += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                CurrentCombatTimeOut = 0;

                while (raycastReached == false)
                {
                    Vector3 playerPosition = playerMovment.transform.position;
                    Vector3 playerHeadPosition = new Vector3(playerPosition.x, playerPosition.y + playerHeadHeight, playerPosition.z);

                    if (Vector3.Distance(transform.position, playerPosition) > MaxShootDistance)
                    {
                        yield return raycastTimeOut;
                        continue;
                    }

                    const int minShootDirection = 0;

                    if (-(transform.position - playerPosition).normalized.y < minShootDirection)
                    {
                        yield return raycastTimeOut;
                        continue;
                    }

                    if (Physics.Linecast(transform.position, playerHeadPosition, out var hit))
                    {
                        if (hit.collider.gameObject.layer == playerLayer)
                        {
                            var position = transform.position;
                            Vector3 shootDirection = -(position - playerHeadPosition);

                            Instantiate(_bullet, position, Quaternion.identity).Shoot(shootDirection);
                            OnShoot?.Invoke();

                            raycastReached = true;
                        }
                    }

                    yield return raycastTimeOut;
                }

                raycastReached = false;
            }
        }
    }
}