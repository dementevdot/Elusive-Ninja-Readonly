using Game;
using Global;
using UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.ScreenHandlers
{
    public class Pause : ButtonsBinder
    {
        [SerializeField] private Button _returnButton;
        [SerializeField] private Button _menuButton;

        private SlowMotionHandler _slowMotionHandler;

        public void Init(SlowMotionHandler slowMotionHandler)
        {
            _slowMotionHandler = slowMotionHandler;
        }

        private void Awake()
        {
            AddBind(_returnButton, OnReturnButtonClick);
            AddBind(_menuButton, OnMenuButtonClick);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _slowMotionHandler.Pause(true);
        }

        private void OnReturnButtonClick()
        {
            GameUIHandler.Instance.SetActiveScreen(GameUIHandler.GameUI);
            _slowMotionHandler.Pause(false);
        }

        private void OnMenuButtonClick()
        {
            SceneLoader.LoadScene(SceneLoader.Menu);
            _slowMotionHandler.Stop();
        }
    }
}
