using System;
using System.Collections;
using Global;
using UnityEngine;
using Game.Enemy;

namespace Game.Player
{
    public class PlayerSkinView : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _deadModel;

        private GameObject _currentSkin;
        private PlayerHealth _playerHealth;

        public event Action SkinChanged;

        public Animator Animator => _animator;

        private void Awake()
        {
            _playerHealth = GetComponent<PlayerHealth>();

            PlayerPrefsService.CurrentSkin.ValueChanged += ChangeSkin;
            if (_playerHealth != null) _playerHealth.HealthIsZero += OnPlayerDied;

            if (_parent.childCount != 0)
                _currentSkin = _parent.GetChild(0).gameObject;

            ChangeSkin(PlayerPrefsService.CurrentSkin.Value);  
        }

        private void OnDisable()
        {
            PlayerPrefsService.CurrentSkin.ValueChanged -= ChangeSkin;
            if (_playerHealth != null) _playerHealth.HealthIsZero -= OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            Destroy(_currentSkin);

            var deadModel = Instantiate(_deadModel, _parent);

            deadModel.GetComponentInChildren<Rigidbody>().velocity = Bullet.PlayerHitVelocity;
        }

        private void ChangeSkin(Skin skin)
        {
            if (_currentSkin != null)
                Destroy(_currentSkin);

            _currentSkin = Instantiate(PlayerSkin.Skins[skin].Prefab, _parent);
            SkinChanged?.Invoke();

            StartCoroutine(UpdateAnimator());
        }

        private IEnumerator UpdateAnimator()
        {
            yield return new WaitForEndOfFrame();

            _animator.Rebind();
        }
    }
}
