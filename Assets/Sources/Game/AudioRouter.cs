using Global;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class AudioRouter : MonoBehaviour
    {
        private AudioSource _audio;

        protected virtual void Awake()
        {
            _audio = GetComponent<AudioSource>();
            _audio.volume = PlayerPrefsService.SfxVolume.Value;
        }

        protected virtual void OnEnable()
        {
           PlayerPrefsService.SfxVolume.ValueChanged += SetVolume;
        }

        protected virtual void OnDisable()
        {
            PlayerPrefsService.SfxVolume.ValueChanged -= SetVolume;
        }

        protected void PlayAudio(AudioClip clip)
        {
            _audio.Stop();
            _audio.clip = clip;
            _audio.Play();
        }

        private void SetVolume(float volume) => _audio.volume = volume;
    }
}
