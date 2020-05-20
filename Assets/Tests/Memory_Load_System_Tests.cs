using CardGame.SaveSystem;
using NUnit.Framework;

namespace Tests
{
    public class Memory_Load_System_Tests : BaseSaveSystemTest<MemorySaveLoadSystem>
    {
        [SetUp]
        public void Setup() => CreateSut();

        #region Values Tests
        
        [Test]
        public void Load_Returns_GameState_Object() 
        {
            Assert.IsInstanceOf<GameState>(GameState);
        }
        
        [Test]
        public void Load_GameState_TimeRemaining_returns_Float() 
        {
            var timeReaRemaining = GameState.TimeRemaining;

            Assert.IsInstanceOf<float>(timeReaRemaining);
        }
        
        [Test]
        public void Load_GameState_Mute_returns_Bool() 
        {
            var mute = GameState.Mute;

            Assert.IsInstanceOf<bool>(mute);
        }
        
        [Test]
        public void Load_GameState_MusicVolume_returns_Float() 
        {
            var musicVolume = GameState.MusicVolume;

            Assert.IsInstanceOf<float>(musicVolume);
        }
        
        [Test]
        public void Load_GameState_PlayerName_returns_String() 
        {
            var playerName = GameState.PlayerName;

            Assert.IsInstanceOf<string>(playerName);
        }
        
        [Test]
        public void Load_GameState_Score_returns_Int() 
        {
            var score = GameState.Score;

            Assert.IsInstanceOf<int>(score);
        }
        
        [Test]
        public void DeleteAll_Returns_Null_GameState()
        {
            System.DeleteAll();
            GameState = System.Load();
            
            Assert.IsNull(GameState);
        }

        #endregion
        
        #region Load Tests

        [Test]
        public void When_PlayerName_Is_Dude_Load_Returns_Dude_For_GameState_Param()
        {
            Assert.AreEqual("Dude", GameState.PlayerName);
        }
        
        [Test]
        public void When_TimeRemaining_Is_2_Load_Returns_2_For_GameState_Param()
        {
            Assert.AreEqual(2f, GameState.TimeRemaining);
        }
        
        [Test]
        public void When_Mute_Is_False_Load_Returns_False_For_GameState_Param()
        {
            Assert.AreEqual(false, GameState.Mute);
        }
        
        [Test]
        public void When_Score_Is_20_Load_Returns_20_For_GameState_Param()
        {
            Assert.AreEqual(20, GameState.Score);
        }
        
        [Test]
        public void When_MusicVolume_Is_1f_Load_Returns_1f_For_GameState_Param()
        {
            Assert.AreEqual(1f, GameState.MusicVolume);
        }
        
        #endregion
    }
}