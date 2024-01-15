using System;
using Global;
using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerCombat : MonoBehaviour
    {
        private const int _magnitudeToDamage = 25;

        [SerializeField] private uint _damage = 3;
        [SerializeField] private CombatCollider _attackCollider;
        [SerializeField] private CombatCollider _damageCollider;

        private PlayerMovment _movment;
        private PlayerHealth _health;

        public event Action OnPunch;

        public uint EnemiesKilled { get; private set; }

        public void Init(PlayerMovment movment, PlayerHealth health)
        {
            _movment = movment;
            _health = health;
        }

        private void OnEnable()
        {
            _attackCollider.OnEnter += AttackHandler;
            _attackCollider.OnStay += AttackHandler;
            _damageCollider.OnDamage += DamageHandler;
        }

        private void OnDisable()
        {
            _attackCollider.OnEnter -= AttackHandler;
            _attackCollider.OnStay -= AttackHandler;
            _damageCollider.OnDamage -= DamageHandler;
        }

        private void AttackHandler(Collider target)
        {
            if (target.TryGetComponent<CombatCollider>(out var combatCollider) == false) return;
            if (_movment.Rigidbody.velocity.magnitude < _magnitudeToDamage && combatCollider.Type == CombatColliderType.Bullet) return;

            if (combatCollider.Type == CombatColliderType.Enemy)
            {
                if (combatCollider.transform.parent.parent.TryGetComponent<Enemy.Enemy>(out var enemy))
                {
                    if (enemy.Died == false)
                    {
                        PlayerPrefsService.EnemiesKilled.Value++;
                        EnemiesKilled++;
                    }
                }
            }

            Attack(combatCollider, _damage);
        }

        private void DamageHandler(uint damage, GameObject sender)
        {
            if (_movment.Rigidbody.velocity.magnitude < _magnitudeToDamage)
                _health.TakeDamage(damage);

            if (sender.TryGetComponent<CombatCollider>(out var enemy) == true)
                enemy.TakeDamage(_damage, gameObject);
        }

        private void Attack(CombatCollider enemy, uint damage)
        {
            enemy.TakeDamage(damage, gameObject);
            OnPunch?.Invoke();
        }
    }
}