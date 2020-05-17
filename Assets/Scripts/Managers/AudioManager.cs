using UnityEngine;

namespace CardGame
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private AudioSource _musicSource;

        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;

            _musicSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip clip, float volume = 1f)
        {
            if (_musicSource) _musicSource.PlayOneShot(clip, volume);
        }

        public void SetVolume(float value)
        {
            _musicSource.volume = value;
        }
        
        public void SetMute(bool value)
        {
            _musicSource.mute = value;
        }
    }
}