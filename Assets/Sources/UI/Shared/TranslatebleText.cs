using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shared
{
    [Serializable]
    public class TranslatebleText
    {
        [SerializeField] private string _russian;
        [SerializeField] private string _english;
        [SerializeField] private string _turkish;
        [SerializeField] private Text _text;

        private Language _currentLanguage;

        public void Init(Language language)
        {
            _text.text = GetTextByLanguage(language);
            _currentLanguage = language;
        }

        public void OnLanguageChanged(Language language)
        {
            if (language == _currentLanguage) return;

            _currentLanguage = language;
            _text.text = GetTextByLanguage(language);
        }

        private string GetTextByLanguage(Language language)
        {
            return language switch
            {
                Language.Russian => _russian,
                Language.English => _english,
                Language.Turkish => _turkish,
                _ => throw new InvalidOperationException(nameof(language))
            };
        }
    }
}