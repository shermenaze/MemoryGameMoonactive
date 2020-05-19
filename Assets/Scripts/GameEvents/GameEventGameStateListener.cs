using GameEvents;
using UnityEngine;

namespace CardGame
{
    public class GameEventGameStateListener : GameEventListenerBase
    {
        [SerializeField] private GameEventGameState _gameEventGameState;
        [SerializeField] private GameStateEvent _eventResponse;

        protected override GameEvent GameEvent => _gameEventGameState;
        
        protected override void OnEventRaised() => _eventResponse?.Invoke(_gameEventGameState.CurrentGameState);
    }
}