using System;
using UnityEngine;

namespace Game
{
    public sealed class CombatCollider : MonoBehaviour
    {
        [SerializeField] private CombatColliderType _type;

        public event Action<Collider> OnEnter;
        public event Action<uint, GameObject> OnDamage;
        public event Action<Collider> OnStay;

        public CombatColliderType Type => _type;

        private void OnTriggerEnter(Collider collider) => OnEnter?.Invoke(collider);
        private void OnTriggerStay(Collider collider) => OnStay?.Invoke(collider);
        public void TakeDamage(uint damage, GameObject sender) => OnDamage?.Invoke(damage, sender);
    }
}
