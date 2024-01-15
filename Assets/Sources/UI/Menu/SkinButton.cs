using UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    [RequireComponent(typeof(Button))]
    public class SkinButton : ButtonsBinder
    {
        private Button _skinUiButton;

        private void Awake()
        {
            _skinUiButton = GetComponent<Button>();
            AddBind(_skinUiButton, () => MenuUIHandler.Instance.SetActiveScreen(MenuUIHandler.SkinUI));
        }
    }
}
