using Global;
using UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class LeaderboardCell : MonoBehaviour
    {
        [SerializeField] private Text _rank;
        [SerializeField] private Text _name;
        [SerializeField] private Text _score;

        public void Init(int rank, string name, int score)
        {
            _rank.text = rank.ToString();
            _score.text = score.ToString();

            if (string.IsNullOrEmpty(name))
            {
                OnLanguageChanged(PlayerPrefsService.Language.Value);

                PlayerPrefsService.Language.ValueChanged += OnLanguageChanged;

                return;
            }

            _name.text = name;
        }

        private void OnLanguageChanged(Language language)
        {
            _name.text = language switch
            {
                Language.Russian => "Аноним",
                Language.English => "Anonymous",
                Language.Turkish => "Anonim",
                _ => _name.text
            };
        }
    }
}
