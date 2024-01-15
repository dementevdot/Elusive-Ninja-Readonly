using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Enemy
{
    [RequireComponent(typeof(EnemyCombat))]
    public class EnemyTimerView : MonoBehaviour
    {
        [SerializeField] private GameObject _timeOutView;
        [SerializeField] private Image _fill;
        [SerializeField] private Image _background;

        private Enemy _enemy;
        private EnemyCombat _enemyCombat;
        private Coroutine _timeOutViewCoroutine;
        private bool _isShooted;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _enemyCombat = GetComponent<EnemyCombat>();
        }

        private void OnEnable()
        {
            _enemyCombat.OnShoot += SetShoot;
            _enemyCombat.DamageCollider.OnDamage += OnDamage;

            _timeOutViewCoroutine = StartCoroutine(TimeOutView());
        }

        private void OnDisable()
        {
            _enemyCombat.OnShoot -= SetShoot;
            _enemyCombat.DamageCollider.OnDamage -= OnDamage;
        }

        private void SetShoot() => _isShooted = true;

        private void OnDamage(uint useless, GameObject useless2)
        {
            StopCoroutine(_timeOutViewCoroutine);
        }

        private IEnumerator TimeOutView()
        {
            while (_enemy.Died == false)
            {
                _fill.fillAmount = 0;

                _timeOutView.SetActive(false);

                while (_enemyCombat.CurrentCombatTimeOut == 0)
                {
                    yield return new WaitForEndOfFrame();
                }

                _timeOutView.SetActive(true);

                while (_enemyCombat.CurrentCombatTimeOut > 0)
                {
                    float normalizedTime = _enemyCombat.CurrentCombatTimeOut / EnemyCombat.MaxCombatTimeOut;
                    _fill.fillAmount = normalizedTime;
                    _background.fillAmount = 1 - normalizedTime;
                    yield return new WaitForEndOfFrame();
                }

                _fill.fillAmount = 1;
                _background.fillAmount = 0;

                while (_isShooted == false)
                {
                    yield return new WaitForEndOfFrame();
                }

                _isShooted = false;
            }
        }
    }
}