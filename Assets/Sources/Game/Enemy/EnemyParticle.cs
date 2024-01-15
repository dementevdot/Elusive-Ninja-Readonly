using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _blood;

        private Enemy _enemy;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void OnEnable()
        {
            _enemy.OnDie += OnDie;
        }

        private void OnDisable()
        {
            _enemy.OnDie -= OnDie;
        }

        private void OnDie() => _blood.Play();
    }
}
