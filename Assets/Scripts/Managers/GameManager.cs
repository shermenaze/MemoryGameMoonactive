using UnityEngine;

namespace CardGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        public GameConfig GameConfig => _gameConfig;

        [SerializeField] private Camera _camera;
        [SerializeField] private InputManager _inputManager;

        private void Awake()
        {
            //_camera = Instantiate(_gameConfig.Camera);
            //_inputManager = Instantiate(_gameConfig.InputManager, transform);
            _inputManager.Init(_camera);
        }

        public void SetInputManagerEnabledState()
        {
            _inputManager.enabled = !_inputManager.enabled;
        }
    }
}