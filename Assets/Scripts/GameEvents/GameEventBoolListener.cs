using UnityEngine;

namespace CardGame
{
    public class GameEventBoolListener : GameEventListenerBase
    {
        [SerializeField] private GameEventBool _gameEventBool;
        [SerializeField] private BoolEvent _eventResponse;

        protected override GameEvent GameEvent => _gameEventBool;


        protected override void OnEventRaised()
        {
            _gameEventBool._condition = !_gameEventBool._condition;
            _eventResponse?.Invoke(_gameEventBool._condition);
        }
    }
}