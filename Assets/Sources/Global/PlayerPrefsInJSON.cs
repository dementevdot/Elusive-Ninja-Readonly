using System;
using System.Collections.Generic;
using UI.Shared;
using UnityEngine;

namespace Global
{
    [Serializable]
    public class PlayerPrefsInJSON
    {
        [SerializeField] private uint _coins = PlayerPrefsService.Coins.Value;
        [SerializeField] private uint _currentHealth = PlayerPrefsService.CurrentHealth.Value;
        [SerializeField] private Skin _currentSkin = PlayerPrefsService.CurrentSkin.Value;
        [SerializeField] private List<Skin> _unlockedSkins = PlayerPrefsService.UnlockedSkins.Value;
        [SerializeField] private uint _stage = PlayerPrefsService.Stage.Value;
        [SerializeField] private uint _level = PlayerPrefsService.Level.Value;
        [SerializeField] private float _musicVolume = PlayerPrefsService.MusicVolume.Value;
        [SerializeField] private float _sfxVolume = PlayerPrefsService.SfxVolume.Value;
        [SerializeField] private Language _language = PlayerPrefsService.Language.Value;
        [SerializeField] private uint _enemiesKilled = PlayerPrefsService.EnemiesKilled.Value;
        [SerializeField] private bool _isNew = PlayerPrefsService.IsNew.Value;

        public void SetPlayerPrefs()
        {
            PlayerPrefsService.Coins.Value = _coins;
            PlayerPrefsService.CurrentHealth.Value = _currentHealth;
            PlayerPrefsService.CurrentSkin.Value = _currentSkin;
            PlayerPrefsService.UnlockedSkins.Value = _unlockedSkins;
            PlayerPrefsService.Stage.Value = _stage;
            PlayerPrefsService.Level.Value = _level;
            PlayerPrefsService.MusicVolume.Value = _musicVolume;
            PlayerPrefsService.SfxVolume.Value = _sfxVolume;
            PlayerPrefsService.Language.Value = _language;
            PlayerPrefsService.EnemiesKilled.Value = _enemiesKilled;
            PlayerPrefsService.IsNew.Value = _isNew;
        }
    }
}