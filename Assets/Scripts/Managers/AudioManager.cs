using UnityEngine;

namespace CardGame
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        public AudioSource AudioSource => _audioSource;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;

            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip clip, float volume = 1f)
        {
            if (_audioSource) _audioSource.PlayOneShot(clip, volume);
        }

        public void SetVolume(float value)
        {
            _audioSource.volume = value;
        }
        
        public void SetMute(bool value)
        {
            _audioSource.mute = value;
        }
    }
}