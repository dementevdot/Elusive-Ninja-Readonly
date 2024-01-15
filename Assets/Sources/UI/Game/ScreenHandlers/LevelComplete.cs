using System;
using Agava.YandexGames;
using Game;
using Game.Level;
using Game.Player;
using Global;
using Shared;
using UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.ScreenHandlers
{
    public class LevelComplete : ButtonsBinder
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Text _killedEnemiesCount;
        [SerializeField] private Text _killedEnemiesReward;
        [SerializeField] private Text _levelCompleteReward;

        private SlowMotionHandler _slowMotionHandler;
        private PlayerCombat _playerCombat;

        public void Init(SlowMotionHandler slowMotionHandler, PlayerCombat playerCombat)
        {
            _slowMotionHandler = slowMotionHandler;
            _playerCombat = playerCombat;
        }

        private void Awake()
        {
            #if !UNITY_EDITOR
                AddBind(_nextLevelButton, () => ShowAd(() => SceneLoader.LoadScene(SceneLoader.Game)));
                AddBind(_menuButton, () => ShowAd(() => SceneLoader.LoadScene(SceneLoader.Menu)));
            #else
                AddBind(_nextLevelButton, () => SceneLoader.LoadScene(SceneLoader.Game));
                AddBind(_menuButton, () => SceneLoader.LoadScene(SceneLoader.Menu));
            #endif
        }

        private void ShowAd(Action actionAfterAd)
        {
            InterstitialAd.Show(
                onOpenCallback: MusicHandler.Mute,
                onCloseCallback: isValid =>
                {
                    if (isValid == false) return;
                    MusicHandler.Unmute();
                    actionAfterAd.Invoke();
                },
                onErrorCallback: useless =>
                {
                    MusicHandler.Unmute();
                    actionAfterAd.Invoke();
                });
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (LevelBuilder.CurrentLevelNumber == 3)
            {
                ReviewPopup.CanOpen((canOpen, _) =>
                {
                    if (canOpen == true)
                    {
                        ReviewPopup.Open();
                    }
                });
            }

            const float rewardMultiplier = 0.5f;
            const uint rewardForLevelComplete = 10;
            uint rewardForEnemies = (uint)Mathf.FloorToInt(_playerCombat.EnemiesKilled * rewardMultiplier);

            if (PlayerPrefsService.IsNew.Value == true)
                PlayerPrefsService.IsNew.Value = false;

            _slowMotionHandler.Pause(true);

            _killedEnemiesCount.text = $"X{_playerCombat.EnemiesKilled}";
            _killedEnemiesReward.text = $"+{rewardForEnemies}";
            _levelCompleteReward.text = $"+{rewardForLevelComplete}";

            PlayerWallet.AddCoins(rewardForEnemies + rewardForLevelComplete);
        }
    }
}
