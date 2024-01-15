using Game.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.Player
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image[] _images;

        private PlayerHealth _health;

        public void Init(PlayerHealth health)
        {
            _health = health;
        }

        private void OnEnable()
        {
            UpdateView();
            _health.HealthChanged += UpdateView;
        }

        private void OnDisable()
        {
            _health.HealthChanged -= UpdateView;
        }

        private void UpdateView()
        {
            float heathPercent = (float)_health.CurrentHealth / PlayerHealth.MaxHealth;

            uint imageCount = (uint)Mathf.RoundToInt(_images.Length * heathPercent);

            foreach (var image in _images)
                image.enabled = true;

            for (uint i = imageCount; i < _images.Length; i++)
                _images[i].enabled = false;
        }
    }
}
