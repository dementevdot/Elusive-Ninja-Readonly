using UI.Shared;
using UnityEngine;

namespace UI.Game
{
    public class GameUIHandler : UIHandler
    {
        public const string GameUI = "gameUI";
        public const string PauseUI = "pauseUI";
        public const string LevelEndUI = "levelEndUI";
        public const string DeadUI = "deadUI";

        [SerializeField] private GameObject _gameUI;
        [SerializeField] private GameObject _pauseUI;
        [SerializeField] private GameObject _levelEndUI;
        [SerializeField] private GameObject _deadUI;
    }
}
