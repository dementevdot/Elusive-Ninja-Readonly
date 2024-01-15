using UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    [RequireComponent(typeof(Button))]
    public class SettingsButton : ButtonsBinder
    {
        private Button _settingsButton;

        private void Awake()
        {
            _settingsButton = GetComponent<Button>();
            AddBind(_settingsButton, () => MenuUIHandler.Instance.SetActiveScreen(MenuUIHandler.SettingsUI));
        }
    }
}
