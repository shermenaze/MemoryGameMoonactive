using UnityEngine;
using UnityEngine.Events;

namespace CardGame
{
    /// <summary>The basic game event listener.
    ///		Holds the basic empty game event.
    /// </summary>
    public class GameEventListener : GameEventListenerBase
    {
        /// <summary>The actual game event.</summary>
        [SerializeField] private GameEvent _gameEvent;

        // ReSharper disable once ConvertToAutoProperty
        protected override GameEvent GameEvent => _gameEvent;

        /// <summary>The event response event is unique to every listener.</summary>
        [SerializeField] private UnityEvent _eventResponse;

        /// <summary>Overriding for the basic listener is just a simple event.</summary>
        protected override void OnEventRaised()
        {
            _eventResponse.Invoke();
        }
    }
}