using UnityEngine;

namespace CardGame
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameEventBool _gamePauseEvent;

        private IInput _inputSystem;

        private void Awake() => _inputSystem = new KeyboardInput(_gamePauseEvent);

        private void Update() => _inputSystem.CheckInput(_camera);
    }
}