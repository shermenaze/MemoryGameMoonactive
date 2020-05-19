using UnityEngine;

namespace CardGame
{
    /// <summary>
    /// The base game event listener.
    ///	Defines the game event and the event raised method.
    ///	handles registration and un-registration.
    /// </summary>
    public abstract class GameEventListenerBase : MonoBehaviour
    {
        [SerializeField] protected bool _respondWhenDisabled;
        
        protected abstract GameEvent GameEvent { get; }

        public void OnEnable()
        {
            var gameEvent = GameEvent;
            if (gameEvent != null) gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (_respondWhenDisabled) return;
            
            var gameEvent = GameEvent;
            if (gameEvent != null) gameEvent.UnregisterListener(this);
        }

        protected abstract void OnEventRaised();

        public void BaseOnEventRaised()
        {
            if (!enabled && !_respondWhenDisabled) return;
            OnEventRaised();
        }
    }
}