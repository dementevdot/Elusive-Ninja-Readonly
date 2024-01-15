using System;
using System.Collections.Generic;
using System.Linq;
using Shared;
using UI.Shared;
using UnityEngine;

namespace Global
{
    public static class PlayerPrefsService
    {
        static PlayerPrefsService()
        {
            bool isNew = PlayerPrefs.HasKey(nameof(Coins)) == false;

            Coins = new EventProperty<uint>();
            Coins.ValueChanged += value => PlayerPrefs.SetInt(nameof(Coins), (int)value);
            Coins.Value = isNew ? 0 : (uint)PlayerPrefs.GetInt(nameof(Coins));

            CurrentHealth = new EventProperty<uint>();
            CurrentHealth.ValueChanged += value => PlayerPrefs.SetInt(nameof(CurrentHealth), (int)value);
            CurrentHealth.Value = isNew ? 3 : (uint)PlayerPrefs.GetInt(nameof(CurrentHealth));

            CurrentSkin = new EventProperty<Skin>();
            CurrentSkin.ValueChanged += value => PlayerPrefs.SetInt(nameof(CurrentSkin), (int)value);
            CurrentSkin.Value = isNew ? Skin.Default : Enum.Parse<Skin>(PlayerPrefs.GetInt(nameof(CurrentSkin)).ToString());

            UnlockedSkins = new EventProperty<List<Skin>>();
            UnlockedSkins.ValueChanged += value => PlayerPrefs.SetString(nameof(UnlockedSkins), ConvertListToString(value));
            UnlockedSkins.Value = isNew ? 
                    new List<Skin> { Skin.Default } : ConvertStringToList(PlayerPrefs.GetString(nameof(UnlockedSkins)));

            Stage = new EventProperty<uint>();
            Stage.ValueChanged += value => PlayerPrefs.SetInt(nameof(Stage), (int)value);
            Stage.Value = isNew ? 0 : (uint)PlayerPrefs.GetInt(nameof(Stage));

            Level = new EventProperty<uint>();
            Level.ValueChanged += value => PlayerPrefs.SetInt(nameof(Level), (int)value);
            Level.Value = isNew ? 0 : (uint)PlayerPrefs.GetInt(nameof(Level));

            MusicVolume = new EventProperty<float>();
            MusicVolume.ValueChanged += value => PlayerPrefs.SetFloat(nameof(MusicVolume), value);
            MusicVolume.Value = isNew ? 1 : PlayerPrefs.GetFloat(nameof(MusicVolume));

            SfxVolume = new EventProperty<float>();
            SfxVolume.ValueChanged += value => PlayerPrefs.SetFloat(nameof(SfxVolume), value);
            SfxVolume.Value = isNew ? 1 : PlayerPrefs.GetFloat(nameof(SfxVolume));

            Language = new EventProperty<Language>();
            Language.ValueChanged += value => PlayerPrefs.SetInt(nameof(Language), (int)value);
            Language.Value = isNew
                ? UI.Shared.Language.Russian
                : Enum.Parse<Language>(PlayerPrefs.GetInt(nameof(Language)).ToString());

            EnemiesKilled = new EventProperty<uint>();
            EnemiesKilled.ValueChanged += value => PlayerPrefs.SetInt(nameof(EnemiesKilled), (int)value);
            EnemiesKilled.Value = isNew ? 0 : (uint)PlayerPrefs.GetInt(nameof(EnemiesKilled));

            IsNew = new EventProperty<bool>();
            IsNew.ValueChanged += value => PlayerPrefs.SetInt(nameof(IsNew), value ? 1 : 0);
            IsNew.Value = isNew ? true : PlayerPrefs.GetInt(nameof(IsNew)) == 1;
        }

        public static EventProperty<uint> Coins { get; private set; }
        public static EventProperty<uint> CurrentHealth { get; private set; }
        public static EventProperty<Skin> CurrentSkin { get; private set; }
        public static EventProperty<List<Skin>> UnlockedSkins { get; private set; }
        public static EventProperty<uint> Stage { get; private set; }
        public static EventProperty<uint> Level { get; private set; }
        public static EventProperty<float> MusicVolume { get; private set; }
        public static EventProperty<float> SfxVolume { get; private set; }
        public static EventProperty<Language> Language { get; private set; }
        public static EventProperty<uint> EnemiesKilled { get; private set; }
        public static EventProperty<bool> IsNew { get; private set; }

        private static string ConvertListToString(List<Skin> skins)
        {
            return skins.Aggregate(string.Empty, (current, skin) => current + (int)skin);
        }

        private static List<Skin> ConvertStringToList(string str)
        {
            return str.ToArray().Select(ch => Enum.Parse<Skin>(ch.ToString())).ToList();
        }
    }
}
