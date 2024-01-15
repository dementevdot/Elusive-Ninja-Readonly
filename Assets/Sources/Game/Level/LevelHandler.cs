using System.Collections;
using Game.Camera;
using Game.Player;
using Global;
using SO;
using UI.Game;
using UnityEngine;

namespace Game.Level
{
    [RequireComponent(typeof(StartHandler))]
    [RequireComponent(typeof(CompleteHandler))]
    public class LevelHandler : MonoBehaviour
    {
        private LevelBuilder _levelBuilder;
        private PlayerMovment _playerMovment;
        private PlayerHealth _playerHealth;
        private PlayerTrajectory _playerTrajectory;
        private SlowMotionHandler _slowMotionHandler;
        private CameraFollowingView _cameraFollowingView;
        private StartHandler _startHandler;
        private CompleteHandler _completeHandler;

        private void Start()
        {
            GameUIHandler.Instance.SetAllScreensOff();
        }

        private void OnDisable()
        {
            _playerHealth.HealthIsZero -= OnPlayerDied;
        }

        public void Init(
            LevelBuilder levelBuilder, 
            PlayerMovment playerMovment, 
            SlowMotionHandler slowMotionHandler, 
            CameraFollowingView cameraFollowingView,
            PlayerTrajectory playerTrajectory, 
            PlayerHealth playerHealth)
        {
            _levelBuilder = levelBuilder;
            _playerMovment = playerMovment;
            _slowMotionHandler = slowMotionHandler;
            _cameraFollowingView = cameraFollowingView;
            _playerTrajectory = playerTrajectory;
            _playerHealth = playerHealth;

            _startHandler = GetComponent<StartHandler>();
            _completeHandler = GetComponent<CompleteHandler>();

            _startHandler.Init(_playerMovment, _levelBuilder.CurrentLevel, _cameraFollowingView);

            _startHandler.LevelStarted += () =>
            {
                GameUIHandler.Instance.SetActiveScreen(GameUIHandler.GameUI); 
                _completeHandler.Init(_levelBuilder.CurrentLevel, _playerMovment, _playerTrajectory, _cameraFollowingView);

                _completeHandler.LevelCompleted += () =>
                {
                    GameUIHandler.Instance.SetAllScreensOff();
                    _slowMotionHandler.StopSmooth();
                    SetLevelNext();
                };

                _completeHandler.PlayerOnEnd += () =>
                {
                    GameUIHandler.Instance.SetActiveScreen(GameUIHandler.LevelEndUI);
                };
            };

            _playerHealth.HealthIsZero += OnPlayerDied;
        }

        public void SetLevelNext()
        {
            uint level = PlayerPrefsService.Level.Value;

            if (level < Stage.MaxLevelCount)
            {
                if (level + 1 == Stage.MaxLevelCount)
                {
                    SetStageNext();
                    return;
                }

                level++;
            }
            else
            {
                level = 0;
            }

            PlayerPrefsService.Level.Value = level;
        }

        private void OnPlayerDied()
        {
            _cameraFollowingView.SetFollowing(false);
            _slowMotionHandler.Stop();
            GameUIHandler.Instance.SetAllScreensOff();
            PlayerPrefsService.CurrentHealth.Value = 1;
            StartCoroutine(ShowDeadScreen());
        }

        private void SetStageNext()
        {
            uint stage = PlayerPrefsService.Stage.Value;

            if (stage + 1 >= _levelBuilder.Stages.Length) return;

            ResetLevel();

            stage++;
            PlayerPrefsService.Stage.Value = stage;
        }

        private void ResetLevel()
        {
            PlayerPrefsService.Level.Value = 0;
            PlayerPrefsService.CurrentHealth.Value = PlayerHealth.MaxHealth;
        }

        private IEnumerator ShowDeadScreen()
        {
            yield return new WaitForSeconds(2f);

            _slowMotionHandler.Pause(true);
            GameUIHandler.Instance.SetActiveScreen(GameUIHandler.DeadUI);
        }
    }
}
