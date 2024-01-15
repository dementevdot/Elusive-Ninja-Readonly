using System;
using System.Collections;
using Game.Camera;
using Game.Player;
using UnityEngine;

namespace Game.Level
{
    public class CompleteHandler : MonoBehaviour
    {
        private PlayerMovment _playerMovment;
        private Level _currentLevel;
        private PlayerTrajectory _playerTrajectory;
        private CameraFollowingView _cameraFollowingView;
        private Coroutine _runCoroutine;

        public event Action LevelCompleted;
        public event Action PlayerOnEnd;

        public void Init(Level currentLevel, PlayerMovment playerMovment, PlayerTrajectory playerTrajectory, CameraFollowingView cameraFollowingView)
        {
            _currentLevel = currentLevel;
            _playerMovment = playerMovment;
            _playerTrajectory = playerTrajectory;
            _cameraFollowingView = cameraFollowingView;

            StartCoroutine(LevelCompleteCheck());
        }

        private IEnumerator LevelCompleteCheck()
        {
            var waitingTime = new WaitForSecondsRealtime(0.5f);

            while (Vector3.Distance(_playerMovment.Transform.position, _currentLevel.EndStartPosition) > _currentLevel.EndStartRadius)
                yield return waitingTime;

            LevelCompleted?.Invoke();

            MovePlayerToGround();
        }

        private void MovePlayerToGround()
        {
            _playerMovment.TurnOffRigidbody();
            _playerTrajectory.TurnOffLineRenderer();

            const float runSpeed = 12f;

            _runCoroutine = StartCoroutine(
                _playerMovment.Run(
                    _playerMovment.Transform.position, 
                    _currentLevel.EndStartPosition, 
                    runSpeed, 
                    MovePlayerToLevelEnd));
        }

        private void MovePlayerToLevelEnd()
        {
            _cameraFollowingView.SetFollowing(false);

            if (_runCoroutine != null)
                StopCoroutine(_runCoroutine);

            _runCoroutine = StartCoroutine(
                _playerMovment.Run(
                    _currentLevel.EndStartPosition,
                    _currentLevel.EndPosition, 
                    _playerMovment.RunSpeed,
                    () => { PlayerOnEnd?.Invoke(); }));
        }
    }
}