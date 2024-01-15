using Agava.YandexGames;
using Global;
using Shared;
using UnityEngine;

namespace Start
{
    public class InitSetYandexCloudSave : Initializable
    {
        [SerializeField] private InitYandexSDK _initYandexSDK;

        private void Start()
        {
            if (_initYandexSDK.IsInited == false)
                _initYandexSDK.Inited += OnInitYandexSDK;
            else
                OnInitYandexSDK();
        }

        private void OnInitYandexSDK()
        {
            #if !UNITY_EDITOR
            PlayerAccount.GetCloudSaveData(GetPlayerData);
            return;
            #endif

            Inited?.Invoke();
        }

        private void GetPlayerData(string data)
        {
            Debug.Log(data);

            if (data == "{}")
                PlayerAccount.SetCloudSaveData(PlayerPrefsToJSON.GetPlayerPrefsInJSON());
            else
                PlayerPrefsToJSON.SetPlayerPrefsByJSON(data);

            PlayerPrefsService.Coins.ValueChanged += u => SetPlayerData();
            PlayerPrefsService.Language.ValueChanged += u => SetPlayerData();
            PlayerPrefsService.Stage.ValueChanged += u => SetPlayerData();
            PlayerPrefsService.Level.ValueChanged += u => SetPlayerData();
            PlayerPrefsService.CurrentHealth.ValueChanged += u => SetPlayerData();
            PlayerPrefsService.MusicVolume.ValueChanged += u => SetPlayerData();
            PlayerPrefsService.SfxVolume.ValueChanged += u => SetPlayerData();
            PlayerPrefsService.CurrentSkin.ValueChanged += u => SetPlayerData();
            PlayerPrefsService.Level.ValueChanged += u => SetLeaderboard();

            Inited?.Invoke();
        }

        private void SetPlayerData()
        {
            PlayerAccount.SetCloudSaveData(PlayerPrefsToJSON.GetPlayerPrefsInJSON());
        }

        private void SetLeaderboard()
        {
            Leaderboard.SetScore("enemiesKilled", (int)PlayerPrefsService.EnemiesKilled.Value);
        }
    }
}