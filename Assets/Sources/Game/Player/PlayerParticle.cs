using UnityEngine;

namespace Game.Player
{
    [RequireComponent(typeof(PlayerCombat))]
    [RequireComponent(typeof(PlayerMovment))]
    public class PlayerParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _punchParticle;
        [SerializeField] private ParticleSystem _jumpParticle;
        [SerializeField] private ParticleSystem _bloodParticle;
        [SerializeField] private ParticleSystem _hearthParticle;

        private PlayerCombat _combat;
        private PlayerMovment _movment;
        private PlayerHealth _health;

        public void Init(PlayerCombat combat, PlayerMovment movment, PlayerHealth health)
        {
            _combat = combat;
            _movment = movment;
            _health = health;

            _combat.OnPunch += _punchParticle.Play;
            _movment.OnJump += _jumpParticle.Play;
            _health.Damaged += _bloodParticle.Play;
            _health.Damaged += _hearthParticle.Play;
        }

        private void OnDisable()
        {
            _combat.OnPunch -= _punchParticle.Play;
            _movment.OnJump -= _jumpParticle.Play;
            _health.Damaged -= _bloodParticle.Play;
            _health.Damaged -= _hearthParticle.Play;
        }
    }
}
