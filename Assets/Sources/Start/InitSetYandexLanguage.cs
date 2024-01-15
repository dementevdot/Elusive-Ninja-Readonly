using Agava.YandexGames;
using Global;
using Shared;
using UI.Shared;
using UnityEngine;

namespace Start
{
    public class InitSetYandexLanguage : Initializable
    {
        [SerializeField] private InitSetYandexCloudSave _initSetYandexCloudSave;

        private void Start()
        {
            #if !UNITY_EDITOR
            if (_initSetYandexCloudSave.IsInited == false)
                _initSetYandexCloudSave.Inited += OnYandexCloudSaveSet;
            else
                OnYandexCloudSaveSet();

            return;
            #endif

            Inited.Invoke();
        }

        private void OnYandexCloudSaveSet()
        {
            Language lang = YandexGamesSdk.Environment.i18n.lang switch
            {
                "ru" => Language.Russian,
                "en" => Language.English,
                "tr" => Language.Turkish,
                _ => Language.English
            };

            PlayerPrefsService.Language.Value = lang;

            Inited.Invoke();
        }
    }
}