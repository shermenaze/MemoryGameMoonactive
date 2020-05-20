using System;
using UnityEngine;

namespace CardGame.SaveSystem
{
    public class SaveLoadManager : MonoBehaviour
    {
        #region Fields

        [Header("Save System")]
        [SerializeField] private PickSaveSystem _pickSaveSystem;
        
        [Space]
        [SerializeField] private BaseSaveSystem _inMemorySaveSystem;
        [SerializeField] private BaseSaveSystem _playerPrefSaveSystem;

        [Header("Save and Load Events")]
        [SerializeField] private GameEventGameState _loadGameEvent;
        [SerializeField] private GameEventGameState _saveGameEvent;

        private BaseSaveSystem _saveSystem;
        private enum PickSaveSystem { PlayerPrefSaveSystem, MemorySaveSystem }
        
        #endregion

        /// <summary>
        /// Loads a save system from the available list
        /// </summary>
        private void Awake()
        {
            BaseSaveSystem saveSystem;
            
            switch (_pickSaveSystem)
            {
                case PickSaveSystem.PlayerPrefSaveSystem:
                    saveSystem = _playerPrefSaveSystem;
                    break;
                case PickSaveSystem.MemorySaveSystem:
                    saveSystem = _inMemorySaveSystem;
                    break;
                default:
                    saveSystem = _playerPrefSaveSystem;
                    break;
            }

            _saveSystem = saveSystem;
        }

        /// <summary>
        /// Creates a new GameState, collects data from all listeners, and saves
        /// using the chosen SaveSystem
        /// </summary>
        public void Save()
        {
            try
            {
                var gameState = new GameState();
            
                _saveGameEvent.Raise(gameState);
                _saveSystem.Save(gameState);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Loads a game state from the current SaveSystem, and distribute it to all listeners
        /// </summary>
        public void Load()
        {
            try
            {
                var gameState = _saveSystem.Load();
                _loadGameEvent.Raise(gameState);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}