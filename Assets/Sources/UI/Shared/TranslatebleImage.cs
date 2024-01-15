using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shared
{
    [Serializable]
    public class TranslatebleImage
    {
        [SerializeField] private Sprite _russian;
        [SerializeField] private Sprite _english;
        [SerializeField] private Sprite _turkish;
        [SerializeField] private Image _image;

        private Language _currentLanguage;

        public void Init(Language language)
        {
            _image.sprite = GetSpriteByLanguage(language);
        }

        public void OnLanguageChanged(Language language)
        {
            if (language == _currentLanguage) return;

            _currentLanguage = language;
            _image.sprite = GetSpriteByLanguage(language);
        }

        private Sprite GetSpriteByLanguage(Language language)
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