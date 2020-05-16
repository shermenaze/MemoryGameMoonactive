using UnityEngine;

namespace CardGame
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private AudioSource _sfxSource;

        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;

            _sfxSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip clip, float volume = 1f)
        {
            if (_sfxSource) _sfxSource.PlayOneShot(clip, volume);
        }
    }
}