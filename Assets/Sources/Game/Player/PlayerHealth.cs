using System;
using Global;
using Shared;
using UnityEngine;

namespace Game.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public const uint MaxHealth = 3;

        private EventProperty<uint> _currentHealth;
        private bool _isDied;

        public event Action Damaged;
        public event Action HealthChanged;
        public event Action HealthIsZero;

        public uint CurrentHealth => _currentHealth.Value;

        private void Awake()
        {
            _currentHealth = PlayerPrefsService.CurrentHealth;
        }

        public void TakeDamage(uint damage)
        {
            if (_isDied == true) return;
            if (damage >= CurrentHealth) _currentHealth.Value = 0;
            if (damage < CurrentHealth) _currentHealth.Value -= damage;
            if (CurrentHealth > MaxHealth) _currentHealth.Value = 0;

            if (CurrentHealth == 0)
            {
                HealthIsZero?.Invoke();
                _isDied = true;
            }

            Damaged?.Invoke();
            HealthChanged?.Invoke();
        }

        public void AddHealth(uint health)
        {
            if (_currentHealth.Value + health > MaxHealth)
                throw new InvalidOperationException();

            _currentHealth.Value += health;

            HealthChanged?.Invoke();
        }
    }
}