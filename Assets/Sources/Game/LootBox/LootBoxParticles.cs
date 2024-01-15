using UnityEngine;

namespace Game.LootBox
{
    public class LootBoxParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosion;
        [SerializeField] private ParticleSystem _money;
        [SerializeField] private LootBox _lootBox;

        private void OnEnable()
        {
            _lootBox.OnDamage += OnDamage;
            _lootBox.OnReward += OnReward;
        }

        private void OnDisable()
        {
            _lootBox.OnDamage -= OnDamage;
            _lootBox.OnReward -= OnReward;
        }

        private void OnDamage() => _explosion.Play();

        private void OnReward() => _money.Play();
    }
}
