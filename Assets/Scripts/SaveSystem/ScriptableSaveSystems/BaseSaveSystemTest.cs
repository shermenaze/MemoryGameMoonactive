using CardGame.SaveSystem;
using UnityEngine;

namespace Tests
{
    public class BaseSaveSystemTest<T> where T: BaseSaveSystem
    {
        protected T System;
        protected GameState GameState;

        protected void CreateSut()
        {
            var gameState = new GameState {
                MusicVolume = 1f,
                Mute = false,
                PlayerName = "Dude",
                Score = 20,
                TimeRemaining = 2f
            };

            System = ScriptableObject.CreateInstance<T>();

            System.Save(gameState);
            GameState = System.Load();
        }
    }
}