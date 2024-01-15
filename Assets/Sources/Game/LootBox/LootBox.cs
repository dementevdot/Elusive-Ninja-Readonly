using System;
using System.Collections;
using Game.Player;
using UnityEngine;

namespace Game.LootBox
{
    public class LootBox : MonoBehaviour
    {
        [SerializeField] private CombatCollider _combatCollider;
        [SerializeField] private bool _randomReward;
        [SerializeField] private uint _maxRandomReward;
        [SerializeField] private bool _hasReward;
        [SerializeField] private uint _reward;

        public event Action OnDamage;
        public event Action OnReward;

        private void Awake()
        {
            if (_randomReward == false) return;

            _hasReward = UnityEngine.Random.Range(0, 2) == 1;

            if (_hasReward)
                _reward = (uint)UnityEngine.Random.Range(1, _maxRandomReward + 1);
        }

        private void OnEnable()
        {
            _combatCollider.OnDamage += TakeDamage;
        }

        private void OnDisable()
        {
            _combatCollider.OnDamage -= TakeDamage;
        }

        private void TakeDamage(uint damage, GameObject sender)
        {
            if (sender.TryGetComponent<PlayerCombat>(out _) == false) return;

            if (_hasReward)
            {
                OnReward?.Invoke();
                PlayerWallet.AddCoins(_reward);
            }

            OnDamage?.Invoke();

            StartCoroutine(DestroyCoroutine());

            GetComponentInChildren<CombatCollider>().gameObject.SetActive(false);
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSecondsRealtime(2f);

            Destroy(gameObject);
        }
    }
}