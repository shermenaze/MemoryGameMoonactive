using System;
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

        private void Awake()
        {
            _saveSystem = Resources.Load<BaseSaveSystem>(_pickSaveSystem.ToString());
        }

        public void Save()
        {
            Debug.Log("Saved!");
            
            var gameState = new GameState
            {
                MusicVolume = _musicVolumeSlider.value,
                Mute = _muteToggle.isOn,
                PlayerName = _nameInput.text,
                TimeRemaining = _hud.Counter,
                Score = 20
            };
            
            Debug.Log(_hud.Counter);
            _saveSystem.Save(gameState);
        }
        
        public void Load()
        {
            var gameState = _saveSystem.Load();
            Debug.Log(_hud.Counter);
            
            Debug.Log($"{gameState.PlayerName} - {gameState.TimeRemaining} - {gameState.MusicVolume}");
        }

        private enum PickSaveSystem{ PlayerPrefSaveSystem, MemorySaveSystem }
    }
}