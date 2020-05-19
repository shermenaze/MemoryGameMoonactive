using CardGame.SaveSystem;
using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Game Event GameState", order = 22)]
    public class GameEventGameState : GameEvent
    {
        public GameState CurrentGameState;

        public void Raise(GameState gameState)
        {
            CurrentGameState = gameState;
            Raise();
        }
    }
}