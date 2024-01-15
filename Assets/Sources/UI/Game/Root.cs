using Game;
using Game.Camera;
using Game.Player;
using Shared;
using UI.Game.ButtonHandlers;
using UI.Game.Player;
using UI.Game.ScreenHandlers;
using UI.Shared;
using UnityEngine;
using Pause = UI.Game.ScreenHandlers.Pause;

namespace UI.Game
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private Pause _pause;
        [SerializeField] private LevelComplete _levelComplete;
        [SerializeField] private HealthView _healthView;
        [SerializeField] private Heal _heal;
        [SerializeField] private SlowMotionHandler _slowMotionHandler;
        [SerializeField] private PlayerCombat _playerCombat;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private ScreenRatio _screenRatio;
        [SerializeField] private OrientationViewHandler[] _orientationViewHandlers;
        [SerializeField] private CameraRatioFOV _cameraRatioFOV;

        private void Awake()
        {
            _pause.Init(_slowMotionHandler);
            _levelComplete.Init(_slowMotionHandler, _playerCombat);
            _healthView.Init(_playerHealth);
            _heal.Init(_playerHealth);
            _cameraRatioFOV.Init(_screenRatio);

            foreach (var orientationViewHandler in _orientationViewHandlers)
                orientationViewHandler.Init(_screenRatio);
        }
    }
}