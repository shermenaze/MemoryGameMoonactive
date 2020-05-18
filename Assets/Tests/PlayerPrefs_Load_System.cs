using CardGame.SaveSystem;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class PlayerPrefs_Load_System
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
                TimeRemaining = 2f
            };
            _system = ScriptableObject.CreateInstance<PlayerPrefsSaveSystem>();
            
            _system.Save(gameState);
            _gameState = _system.Load();
        }

        #region Values Tests

                
        [Test]
        public void Load_Returns_GameState_Object() 
        {
            Assert.IsInstanceOf<GameState>(_gameState);
        }
        
        [Test]
        public void Load_GameState_TimeRemaining_returns_Float() 
        {
            var timeReaRemaining = _gameState.TimeRemaining;

            Assert.IsInstanceOf<float>(timeReaRemaining);
        }
        
        [Test]
        public void Load_GameState_Mute_returns_Bool() 
        {
            var mute = _gameState.Mute;

            Assert.IsInstanceOf<bool>(mute);
        }
        
        [Test]
        public void Load_GameState_MusicVolume_returns_Float() 
        {
            var musicVolume = _gameState.MusicVolume;

            Assert.IsInstanceOf<float>(musicVolume);
        }
        
        [Test]
        public void Load_GameState_PlayerName_returns_String() 
        {
            var playerName = _gameState.PlayerName;

            Assert.IsInstanceOf<string>(playerName);
        }
        
        [Test]
        public void Load_GameState_Score_returns_Int() 
        {
            var score = _gameState.Score;

            Assert.IsInstanceOf<int>(score);
        }
        
        [Test]
        public void DeleteAll_Returns_Null_GameState()
        {
            _system.DeleteAll();
            _gameState = _system.Load();
            
            Assert.IsNull(_gameState);
        }

        #endregion

        #region Load Tests

        [Test]
        public void When_PlayerName_Is_Dude_Load_Returns_Dude_For_GameState_Param()
        {
            Assert.AreEqual("Dude", _gameState.PlayerName);
        }
        
        [Test]
        public void When_TimeRemaining_Is_2_Load_Returns_2_For_GameState_Param()
        {
            Assert.AreEqual(2f, _gameState.TimeRemaining);
        }
        
        [Test]
        public void When_Mute_Is_False_Load_Returns_False_For_GameState_Param()
        {
            Assert.AreEqual(false, _gameState.Mute);
        }
        
        [Test]
        public void When_Score_Is_20_Load_Returns_20_For_GameState_Param()
        {
            Assert.AreEqual(20, _gameState.Score);
        }
        
        [Test]
        public void When_MusicVolume_Is_1f_Load_Returns_1f_For_GameState_Param()
        {
            Assert.AreEqual(1f, _gameState.MusicVolume);
        }
        
        #endregion
    }
}