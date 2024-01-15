using UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    [RequireComponent(typeof(Button))]
    public class CloseSkinButton : ButtonsBinder
    {
        private Button _closeButton;

        private void Awake()
        {
            _closeButton = GetComponent<Button>();
            AddBind(_closeButton, () => MenuUIHandler.Instance.SetActiveScreen(MenuUIHandler.MainUI));
        }
    }
}
