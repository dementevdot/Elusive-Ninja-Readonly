using UnityEngine;

namespace Game.Enemy
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyModelHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _aliveModel;
        [SerializeField] private GameObject _deadModel;

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

        private void OnDie()
        {
            const int recoilMultiplier = 3;

            Destroy(_aliveModel);
            Instantiate(_deadModel, transform)
                .GetComponentInChildren<Rigidbody>()
                .velocity =
                _enemy.LastDamageSender
                .GetComponentInParent<Rigidbody>().velocity * recoilMultiplier;
        }
    }
}
