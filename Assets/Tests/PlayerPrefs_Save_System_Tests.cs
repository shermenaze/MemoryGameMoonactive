using CardGame.SaveSystem;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class PlayerPrefs_Save_System_Tests
    {
        private PlayerPrefsSaveSystem _system;
        private GameState _gameState;
        
        [SetUp]
        public void Setup()
        {
            var gameState = new GameState {
                MusicVolume = 1f,
                Mute = false,
                PlayerName = "Dude",
                Score = 20,
                TimeRemaining = 2.5f
            };
            _system = ScriptableObject.CreateInstance<PlayerPrefsSaveSystem>();
            
            _system.Save(gameState);
            _gameState = _system.Load();
        }

        #region Save Tests

        [Test]
        public void Save_5_To_TimeRemaining_Loads_5_To_GameState_Param()
        {
            _gameState.TimeRemaining = 5;
            
            _system.Save(_gameState);
            
            Assert.AreEqual(5, _system.Load().TimeRemaining);
        }
        
        [Test]
        public void Save_1f_To_MusicVolume_Loads_1f_To_GameState_Param()
        {
            _gameState.MusicVolume = 0.3f;
            
            _system.Save(_gameState);
            
            Assert.AreEqual(0.3f, _system.Load().MusicVolume);
        }
        
        [Test]
        public void Save_False_To_Mute_Loads_False_To_GameState_Param()
        {
            _gameState.Mute = false;
            
            _system.Save(_gameState);
            
            Assert.AreEqual(false, _system.Load().Mute);
        }
        
        [Test]
        public void Save_John_To_PlayerName_Loads_John_To_GameState_Param()
        {
            _gameState.PlayerName = "John";
            
            _system.Save(_gameState);
            
            Assert.AreEqual("John", _system.Load().PlayerName);
        }
        
        [Test]
        public void Save_25_To_Score_Loads_25_To_GameState_Param()
        {
            _gameState.Score = 25;
            
            _system.Save(_gameState);
            
            Assert.AreEqual(25, _system.Load().Score);
        }

        #endregion
    }
}