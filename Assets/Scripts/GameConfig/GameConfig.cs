using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(menuName = "GameConfigs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private Camera _camera;

        public InputManager InputManager => _inputManager;
        public Camera Camera => _camera;
    }   
}