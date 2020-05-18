using CardGame.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame.SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [Header("Save System")]
        [SerializeField] private PickSaveSystem _pickSaveSystem;

        [Header("Items to Change")]
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Toggle _muteToggle;
        [SerializeField] private TMP_InputField _nameInput;
        [SerializeField] private Hud _hud;

        private BaseSaveSystem _saveSystem;
        
        private enum PickSaveSystem { PlayerPrefSaveSystem, MemorySaveSystem }

        private void Awake()
        {
            _saveSystem = Resources.Load<BaseSaveSystem>(_pickSaveSystem.ToString());
        }

        public void Save()
        {
            var gameState = new GameState
            {
                MusicVolume = _musicVolumeSlider.value,
                Mute = _muteToggle.isOn,
                PlayerName = _nameInput.text,
                TimeRemaining = _hud.Counter,
                Score = 20
            };
            
            _saveSystem.Save(gameState);
        }

        public void Load()
        {
            var gameState = _saveSystem.Load();
            
            if (gameState != null)
            {
                _musicAudioSource.volume = gameState.MusicVolume;
                _musicVolumeSlider.value = gameState.MusicVolume;
                _muteToggle.isOn = gameState.Mute;
                _nameInput.text = gameState.PlayerName;
                _hud.Counter = gameState.TimeRemaining;
            }
        }
    }
}