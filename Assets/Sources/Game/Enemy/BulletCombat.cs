using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(Bullet))]
    [RequireComponent(typeof(CombatCollider))]
    public class BulletCombat : MonoBehaviour
    {
        private CombatCollider _combatCollider;
        private Bullet _bullet;

        private void Awake()
        {
            _bullet = GetComponent<Bullet>();
            _combatCollider = GetComponent<CombatCollider>();
        }

        private void OnEnable()
        {
            _combatCollider.OnEnter += AttackHandler;
            _combatCollider.OnDamage += DamageHandler;
        }

        private void OnDisable()
        {
            _combatCollider.OnEnter -= AttackHandler;
            _combatCollider.OnDamage -= DamageHandler;
        }

        private void DamageHandler(uint damage, GameObject sender)
        {
            _bullet.Destroy();
        }

        private void AttackHandler(Collider target)
        {
            if (target.TryGetComponent<CombatCollider>(out var combatCollider) == false)
            {
                const int wallsLayer = 3;

                if (target.gameObject.layer == wallsLayer)
                {
                    _bullet.Destroy();
                }

                return;
            }

            if (combatCollider.Type is CombatColliderType.Enemy or CombatColliderType.LootBox) return;

            _bullet.SetPlayerHitVelocity();
            combatCollider.TakeDamage(_bullet.Damage, gameObject);
        }
    }
}