using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    /// <summary>
    /// The basic game event.
    ///	Registers listeners and raises events.
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObjects/Game Event", order = 20)]
    public class GameEvent : ScriptableObject
    {
        /// <summary>
        /// The list of listeners for this event.
        /// </summary>
        private readonly List<GameEventListenerBase> _listeners = new List<GameEventListenerBase>();

        /// <summary>Adds a listener to the list.</summary>
        /// <param name="listener">The listener to add.</param>
        public void RegisterListener(GameEventListenerBase listener) => _listeners.Add(listener);

        /// <summary>Removes a listener from the list.</summary>
        /// <param name="listener">The listener to remove.</param>
        public void UnregisterListener(GameEventListenerBase listener) => _listeners.Remove(listener);
        
        /// <summary>
        /// Raises this event to all its listeners.
        /// </summary>
        public void Raise()
        {
            for (int listenerIndex = _listeners.Count - 1; listenerIndex >= 0; listenerIndex--)
                _listeners[listenerIndex].BaseOnEventRaised();
        }
    }
}