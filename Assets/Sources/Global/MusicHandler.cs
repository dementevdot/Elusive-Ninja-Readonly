using UnityEngine;

namespace Global
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicHandler : MonoBehaviour
    {
        private static MusicHandler _instance;

        private AudioSource _audio;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(gameObject);

            _audio = GetComponent<AudioSource>();
            PlayerPrefsService.MusicVolume.ValueChanged += SetVolume;
            SetVolume(PlayerPrefsService.MusicVolume.Value);

            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            PlayerPrefsService.MusicVolume.ValueChanged -= SetVolume;
        }

        public static void Mute() => _instance._audio.volume = 0;

        public static void Unmute() => _instance._audio.volume = PlayerPrefsService.MusicVolume.Value;

        private void SetVolume(float volume) => _audio.volume = volume;
    }
}
