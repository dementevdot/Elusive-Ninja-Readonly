using UI.Shared;
using UnityEngine;

namespace UI.Menu
{
    public class MenuUIHandler : UIHandler
    {
        public const string MainUI = "mainUI";
        public const string SkinUI = "skinUI";
        public const string SettingsUI = "settingsUI";
        public const string LeaderboardUI = "leaderboardUI";

        [SerializeField] private GameObject _mainUI;
        [SerializeField] private GameObject _skinUI;
        [SerializeField] private GameObject _settingsUI;
        [SerializeField] private GameObject _leaderboardUI;
    }
}
