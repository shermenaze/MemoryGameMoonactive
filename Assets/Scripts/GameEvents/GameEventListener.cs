using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent GameEvent => _gameEvent;
    
    [SerializeField] private GameEvent _gameEvent;
    [SerializeField] private UnityEvent _eventResponse;

    private void OnEnable()
    {
        if (_gameEvent != null) _gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (_gameEvent != null) _gameEvent.UnregisterListener(this);
    }
    
    public void OnEventRaised()
    {
        if(enabled) _eventResponse.Invoke();
    }
}