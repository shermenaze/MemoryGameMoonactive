using UnityEngine;
using UnityEngine.Events;

namespace CardGame
{
    /// <summary>
    /// The game event listener.
    ///	Holds the empty game event.
    /// </summary>
    public class GameEventListener : GameEventListenerBase
    {
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] private UnityEvent _eventResponse;
        
        protected override GameEvent GameEvent => _gameEvent;

        protected override void OnEventRaised() => _eventResponse?.Invoke();
    }
}