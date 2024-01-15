using System;
using System.Reflection;
using Game.Camera;
using Game.Level;
using Game.Player;
using UnityEngine;

namespace Game
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private PlayerAudio _playerAudio;
        [SerializeField] private PlayerAnimation _playerAnimation;
        [SerializeField] private PlayerCombat _playerCombat;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerMovment _playerMovment;
        [SerializeField] private PlayerParticle _playerParticle;
        [SerializeField] private PlayerSkinView _playerSkinView;
        [SerializeField] private PlayerSlowMotion _playerSlowMotion;
        [SerializeField] private PlayerTrajectory _playerTrajectory;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Rigidbody _playerRigidbody;
        [SerializeField] private LevelBuilder _levelBuilder;
        [SerializeField] private LevelHandler _levelHandler;
        [SerializeField] private SlowMotionHandler _slowMotionHandler;
        [SerializeField] private CameraFollowingView _cameraFollowingView;

        public static PlayerMovment PlayerMovment { get; private set; }

        private void Awake()
        {
            var fieldInfos = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var fieldInfo in fieldInfos)
                if (fieldInfo.GetValue(this) == null)
                    throw new NullReferenceException(fieldInfo.Name);

            PlayerMovment = _playerMovment;

            _playerAnimation.Init(_playerMovment, _playerSkinView, _playerCombat, _playerHealth);
            _playerCombat.Init(_playerMovment, _playerHealth);
            _playerMovment.Init(_playerAnimation, _playerHealth, _playerTransform, _playerRigidbody);
            _playerParticle.Init(_playerCombat, _playerMovment, _playerHealth);
            _playerSlowMotion.Init(_playerMovment, _slowMotionHandler, _playerHealth);
            _playerTrajectory.Init(_playerInput, _playerMovment, _playerHealth);
            _levelHandler.Init(_levelBuilder, _playerMovment, _slowMotionHandler, _cameraFollowingView, _playerTrajectory, _playerHealth);
            _playerAudio.Init(_playerCombat, _playerMovment);
        }
    }
}