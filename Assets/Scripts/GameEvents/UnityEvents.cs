using System;
using CardGame.SaveSystem;
using UnityEngine;
using UnityEngine.Events;

//TODO: Remove unnecessary items
[Serializable] public class BoolEvent : UnityEvent<bool> { }
[Serializable] public class StringEvent : UnityEvent<string> { }
[Serializable] public class IntEvent : UnityEvent<int> { }
[Serializable] public class FloatEvent : UnityEvent<float> { }
[Serializable] public class Vector3Event : UnityEvent<Vector3> { }
[Serializable] public class GameStateEvent : UnityEvent<GameState> { }