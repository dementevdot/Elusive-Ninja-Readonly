using Agava.YandexGames;
using Game;
using Game.Level;
using Game.Player;
using Global;
using UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.ScreenHandlers
{
    public class Dead : ButtonsBinder
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _rewardButton;
        [SerializeField] private LevelHandler _levelHandler; 

        private bool _rewarded;

        private void Awake()
        {
            #if !UNITY_EDITOR
                AddBind(_restartButton, 
                    () => InterstitialAd.Show(
                        onOpenCallback: MusicHandler.Mute, 
                        onCloseCallback: isValid => { 
                            if (isValid == false) return;
                            MusicHandler.Unmute();
                            SceneLoader.LoadScene(SceneLoader.Game);
                        },
                        onErrorCallback: s => { SceneLoader.LoadScene(SceneLoader.Game);}));
                AddBind(_rewardButton,
                    () => VideoAd.Show(
                        onOpenCallback: MusicHandler.Mute,
                        onRewardedCallback: () =>
                        {
                            PlayerPrefsService.Level.Value = (uint)LevelBuilder.CurrentLevelNumber;
                            PlayerPrefsService.CurrentHealth.Value = PlayerHealth.MaxHealth;

                            _levelHandler.NextLevel();

                            _rewarded = true;
                        },
                        onCloseCallback: () =>
                        {
                            if (_rewarded)
                            {
                                MusicHandler.Unmute();
                                SceneLoader.LoadScene(SceneLoader.Game);
                            }
                            else
                            {
                                _rewardButton.enabled = false;
                            }
                        }));
            #else
            AddBind(_restartButton, () => SceneLoader.LoadScene(SceneLoader.Game));
            AddBind(_rewardButton, () =>
            {
                PlayerPrefsService.Level.Value = (uint)LevelBuilder.CurrentLevelNumber;
                PlayerPrefsService.CurrentHealth.Value = PlayerHealth.MaxHealth;

                _levelHandler.SetLevelNext();

                _rewarded = true;
                SceneLoader.LoadScene(SceneLoader.Game);
            });
            #endif
        }
    }
}
