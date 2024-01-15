using System;
using Global;
using UnityEngine;

namespace UI.Shared
{
    public class LanguageHandler : MonoBehaviour
    {
        private static Language Language;

        [SerializeField] private TranslatebleText[] _texts;
        [SerializeField] private TranslatebleImage[] _images;

        private static event Action<Language> LanguageChanged;

        private void Awake()
        {
            Language = PlayerPrefsService.Language.Value;

            foreach (var translatebleText in _texts)
            {
                translatebleText.Init(Language);
                LanguageChanged += translatebleText.OnLanguageChanged;
            }

            foreach (var translatebleImage in _images)
            {
                translatebleImage.Init(Language);
                LanguageChanged += translatebleImage.OnLanguageChanged;
            }
        }

        private void OnDestroy()
        {
            foreach (var translatebleText in _texts)
                LanguageChanged -= translatebleText.OnLanguageChanged;

            foreach (var translatebleImage in _images)
                LanguageChanged -= translatebleImage.OnLanguageChanged;
        }

        public static void SetLanguage(Language language)
        {
            Language = language;
            PlayerPrefsService.Language.Value = language;
            LanguageChanged?.Invoke(Language);
        }
    }
}
