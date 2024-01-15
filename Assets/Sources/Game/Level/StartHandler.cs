using System;
using System.Collections;
using Game.Camera;
using Game.Player;
using UnityEngine;

namespace Game.Level
{
    public class StartHandler : MonoBehaviour
    {
        private PlayerMovment _playerMovment;
        private CameraFollowingView _cameraFollowingView;
        private Level _currentLevel;

        public event Action LevelStarted;

        public void Init(PlayerMovment playerMovment, Level currentLevel, CameraFollowingView cameraFollowingView)
        {
            _playerMovment = playerMovment;
            _currentLevel = currentLevel;
            _cameraFollowingView = cameraFollowingView;

            StartCoroutine(MovePlayerToStart());
        }

        private IEnumerator MovePlayerToStart()
        {
            _playerMovment.SetPlayerPosition(_currentLevel.StartPosition);
            _cameraFollowingView.SetPosition(_currentLevel.StartEndPosition);
            _cameraFollowingView.SetFollowing(false);
            _playerMovment.TurnOffRigidbody();

            yield return new WaitForSeconds(0.3f);

            StartCoroutine(_playerMovment.Run(_currentLevel.StartPosition, _currentLevel.StartEndPosition, _playerMovment.RunSpeed, OnLevelStart));
        }

        private void OnLevelStart()
        {
            _cameraFollowingView.SetFollowing(true);
            _playerMovment.TurnOnRigidbody();

            LevelStarted?.Invoke();
        }
    }
}