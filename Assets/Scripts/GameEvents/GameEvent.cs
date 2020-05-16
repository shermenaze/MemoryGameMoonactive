using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Game Event", order = 20)]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> _listeners = new List<GameEventListener>();

    [ContextMenu("Raise")]
    public void Raise()
    {
        for (int listenerIndex = _listeners.Count - 1; listenerIndex >= 0; listenerIndex--)
            _listeners[listenerIndex]?.OnEventRaised();
    }
    
    public void RegisterListener(GameEventListener listener) => _listeners.Add(listener);

    public void UnregisterListener(GameEventListener listener) => _listeners.Remove(listener);
}