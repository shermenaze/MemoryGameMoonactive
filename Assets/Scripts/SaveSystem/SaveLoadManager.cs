using UnityEngine;

namespace CardGame.SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        [Header("Save System")]
        [SerializeField] private BaseSaveSystem _saveSystem;

        [Header("Items to Change")]
        [SerializeField] private AudioSource _musicAudioSource;
        
        public BaseSaveSystem SaveSystem => _saveSystem;
        
        private GameState _gameState;

        public void Save()
        {
            
        }
        
        public void Load()
        {
            
        }
    }
}