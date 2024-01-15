using Global;
using UI.Shared;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class LanguageSettings : ButtonsBinder
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _russian;
        [SerializeField] private Button _english;
        [SerializeField] private Button _turkish;

        private void Awake()
        {
            AddBind(_closeButton, () => MenuUIHandler.Instance.SetActiveScreen(MenuUIHandler.MainUI));
            AddBind(_russian, OnRussian);
            AddBind(_english, OnEnglish);
            AddBind(_turkish, OnTurkish);

            _russian.enabled = PlayerPrefsService.Language.Value != Language.Russian;
            _english.enabled = PlayerPrefsService.Language.Value != Language.English;
            _turkish.enabled = PlayerPrefsService.Language.Value != Language.Turkish;
        }

        private void OnRussian()
        {
            LanguageHandler.SetLanguage(Language.Russian);
            _russian.enabled = false;
            _english.enabled = true;
            _turkish.enabled = true;
        }

        private void OnEnglish()
        {
            LanguageHandler.SetLanguage(Language.English);
            _russian.enabled = true;
            _english.enabled = false;
            _turkish.enabled = true;
        }

        private void OnTurkish()
        {
            LanguageHandler.SetLanguage(Language.Turkish);
            _russian.enabled = true;
            _english.enabled = true;
            _turkish.enabled = false;
        }
    }
}
