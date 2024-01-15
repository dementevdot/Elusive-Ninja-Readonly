using UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    [RequireComponent(typeof(Button))]
    public class LeaderboardButton : ButtonsBinder
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            AddBind(_button, () => MenuUIHandler.Instance.SetActiveScreen(MenuUIHandler.LeaderboardUI));
        }
    }
}
