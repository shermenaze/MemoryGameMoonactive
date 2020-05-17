using UnityEngine;

namespace CardGame
{
    /// <summary>The base game event listener.
    ///	Defines the game event and the event raised method.
    ///	handles registration and un-registration.
    /// </summary>
    public abstract class GameEventListenerBase : MonoBehaviour
    {
        [SerializeField] protected bool _respondWhenDisabled;

        /// <summary>Every listener must have its event.
        ///	an abstract property allows casting from any derived extensions.
        /// </summary>
        protected abstract GameEvent GameEvent { get; }

        public void OnEnable()
        {
            GameEvent gameEvent = GameEvent;
            if (gameEvent != null) gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (!_respondWhenDisabled)
            {
                GameEvent gameEvent = GameEvent;
                if (gameEvent != null) gameEvent.UnregisterListener(this);
            }
        }

        /// <summary>
        /// Responds to the event being raised.
        ///	An abstract method allows each derived listener to do it differently.
        /// </summary>
        protected abstract void OnEventRaised();

        public void BaseOnEventRaised()
        {
            if (!enabled && !_respondWhenDisabled) return;
            OnEventRaised();
        }
    }
}