using System.Collections.Generic;
using Global;
using UI.Shared;
using UnityEngine;

namespace Shared
{
    public class Cheats : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Backspace) == false) return;

            if (Input.GetKeyDown(KeyCode.Alpha0)) ResetPlayer();
            if (Input.GetKeyDown(KeyCode.Alpha1)) PlayerPrefsService.Coins.Value += 100;
            if (Input.GetKeyDown(KeyCode.Alpha2)) PlayerPrefsService.Level.Value += 1;
            if (Input.GetKeyDown(KeyCode.Alpha3)) PlayerPrefsService.Stage.Value += 1;
        }

        private void ResetPlayer()
        {
            PlayerPrefsService.CurrentHealth.Value = 3;
            PlayerPrefsService.CurrentSkin.Value = Skin.Default;
            PlayerPrefsService.UnlockedSkins.Value = new List<Skin> { Skin.Default };
            PlayerPrefsService.Stage.Value = 0;
            PlayerPrefsService.Level.Value = 0;
            PlayerPrefsService.MusicVolume.Value = 0.1f;
            PlayerPrefsService.SfxVolume.Value = 0.1f;
            PlayerPrefsService.Language.Value = Language.Russian;
            PlayerPrefsService.EnemiesKilled.Value = 0;
            PlayerPrefsService.IsNew.Value = true;
            PlayerPrefsService.Coins.Value = 0;
        }
    }
}
