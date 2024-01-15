using UnityEngine;
using UnityEngine.UI;
using Global;
using SO;

namespace UI.Menu
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Text _currentStage;
        [SerializeField] private Text _nextStage;
        [SerializeField] private Text _levelsCount;

        private void OnEnable()
        {
            OnStageChanged(PlayerPrefsService.Stage.Value);

            PlayerPrefsService.Stage.ValueChanged += OnStageChanged;
            PlayerPrefsService.Level.ValueChanged += OnLevelChanged;
        }

        private void OnDisable()
        {
            PlayerPrefsService.Stage.ValueChanged -= OnStageChanged;
            PlayerPrefsService.Level.ValueChanged -= OnLevelChanged;
        }

        private void OnStageChanged(uint stage)
        {
            _currentStage.text = (stage + 1).ToString();
            _nextStage.text = (stage + 2).ToString();
            OnLevelChanged(PlayerPrefsService.Level.Value);
        }

        private void OnLevelChanged(uint level)
        {
            _levelsCount.text = $"{level + 1} / {Stage.MaxLevelCount}";
        }
    }
}
