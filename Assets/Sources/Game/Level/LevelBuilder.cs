using System;
using Global;
using SO;
using UnityEngine;

namespace Game.Level
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private bool _manualLevelLoad;
        [SerializeField] private Stage[] _stages;

        public static uint? CurrentLevelNumber { get; private set; }
        public Level CurrentLevel { get; private set; }
        public Stage[] Stages => _stages;

        private void Awake()
        {
            if (_manualLevelLoad == false)
                LoadLevel(PlayerPrefsService.Level.Value);
        }

        private void LoadLevel(uint level)
        {
            if (CurrentLevel != null)
                if (CurrentLevelNumber == level) return;

            if (level > Stage.MaxLevelCount)
                throw new InvalidOperationException(nameof(level));

            if (CurrentLevel != null)
                Destroy(CurrentLevel);

            CurrentLevelNumber = level;
            var currentStage = _stages[PlayerPrefsService.Stage.Value];

            CurrentLevel = Instantiate(currentStage.Levels[level], transform.parent).GetComponent<Level>();

            CurrentLevel.SetLevelMaterial(currentStage.InnerMaterial, currentStage.OutsideMaterial);
            RenderSettings.skybox = currentStage.Skybox;
        }
    }
}