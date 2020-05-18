using UnityEngine;

namespace CardGame
{
    /// <summary>An extension of the game event that contains a boolean.</summary>
    [CreateAssetMenu(menuName = "ScriptableObjects/Game Event Bool", order = 22)]
    public class GameEventBool : GameEvent
    {
        public bool _condition;
        [SerializeField] private bool _initialCondition;

        private void OnEnable() => _condition = _initialCondition;

        public void Raise(bool newCondition)
        {
            _condition = newCondition;
            Raise();
        }
    }
}
