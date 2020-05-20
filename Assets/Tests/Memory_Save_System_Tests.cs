using CardGame.SaveSystem;
using NUnit.Framework;

namespace Tests
{
    public class Memory_Save_System_Tests : BaseSaveSystemTest<MemorySaveLoadSystem>
    {
        [SetUp]
        public void Setup() => CreateSut();

        #region Save Tests

        [Test]
        public void Save_5_To_TimeRemaining_Loads_5_To_GameState_Param()
        {
            GameState.TimeRemaining = 5;
            
            System.Save(GameState);
            
            Assert.AreEqual(5, System.Load().TimeRemaining);
        }
        
        [Test]
        public void Save_1f_To_MusicVolume_Loads_1f_To_GameState_Param()
        {
            GameState.MusicVolume = 0.3f;
            
            System.Save(GameState);
            
            Assert.AreEqual(0.3f, System.Load().MusicVolume);
        }
        
        [Test]
        public void Save_False_To_Mute_Loads_False_To_GameState_Param()
        {
            GameState.Mute = false;
            
            System.Save(GameState);
            
            Assert.AreEqual(false, System.Load().Mute);
        }
        
        [Test]
        public void Save_John_To_PlayerName_Loads_John_To_GameState_Param()
        {
            GameState.PlayerName = "John";
            
            System.Save(GameState);
            
            Assert.AreEqual("John", System.Load().PlayerName);
        }
        
        [Test]
        public void Save_25_To_Score_Loads_25_To_GameState_Param()
        {
            GameState.Score = 25;
            
            System.Save(GameState);
            
            Assert.AreEqual(25, System.Load().Score);
        }
        
        #endregion
    }
}