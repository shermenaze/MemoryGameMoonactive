using UnityEngine;

namespace CardGame
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameEvent _gamePauseEvent;

        private Camera _camera;
        private IInput _inputSystem;

        public void Init(Camera camera)
        {
            _camera = camera;
            name = name.Declone();
        }
        
        private void Awake() => _inputSystem = new KeyboardMouseInput(_gamePauseEvent);

        private void Update() => _inputSystem.CheckInput(_camera);
    }
}