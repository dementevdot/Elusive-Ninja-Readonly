using UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.ButtonHandlers
{
    [RequireComponent(typeof(Button))]
    public class Pause : ButtonsBinder
    {
        private Button _pauseButton;

        private void Awake()
        {
            _pauseButton = GetComponent<Button>();
            AddBind(_pauseButton, () => GameUIHandler.Instance.SetActiveScreen(GameUIHandler.PauseUI));
        }
    }
}
