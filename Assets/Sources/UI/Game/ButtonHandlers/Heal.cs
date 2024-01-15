using Game.Player;
using UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.ButtonHandlers
{
    [RequireComponent(typeof(Button))]
    public class Heal : ButtonsBinder
    {
        private const uint HealCost = 40;

        private Button _button;
        private PlayerHealth _playerHealth;

        public void Init(PlayerHealth playerHealth)
        {
            _button = GetComponent<Button>();
            _playerHealth = playerHealth;
            _playerHealth.HealthChanged += ViewHandler;

            AddBind(_button, OnClick);
            ViewHandler();
        }

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= ViewHandler;
        }

        private void OnClick()
        {
            if (PlayerWallet.TryTakeCoins(HealCost) == false) return;

            _playerHealth.AddHealth(1);
        }

        private void ViewHandler()
        {
            gameObject.SetActive(_playerHealth.CurrentHealth != PlayerHealth.MaxHealth);
        }
    }
}
