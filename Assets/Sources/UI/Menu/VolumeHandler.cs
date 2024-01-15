using Global;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class VolumeHandler : MonoBehaviour
    {
        [SerializeField] private Slider _music;
        [SerializeField] private Slider _sfx;

        private void OnEnable()
        {
            _music.value = PlayerPrefsService.MusicVolume.Value;
            _sfx.value = PlayerPrefsService.SfxVolume.Value;

            _music.onValueChanged.AddListener(SetMusicVolume);
            _sfx.onValueChanged.AddListener(SetSfxVolume);
        }

        private void OnDisable()
        {
            _music.onValueChanged.RemoveListener(SetMusicVolume);
            _sfx.onValueChanged.RemoveListener(SetSfxVolume);
        }

        private static void SetMusicVolume(float volume)
        {
            PlayerPrefsService.MusicVolume.Value = volume;
        }

        private static void SetSfxVolume(float volume)
        {
            PlayerPrefsService.SfxVolume.Value = volume;
        }
    }
}
