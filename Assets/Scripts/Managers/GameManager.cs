using CardGame.SaveSystem;
using CardGame.UI;
using UnityEngine;

namespace CardGame
{
    public class GameManager : MonoBehaviour
    {

        #region Fields
        
        [SerializeField] private GameConfig _gameConfig;

        private Camera _camera;
        private InputManager _inputManager;
        private MemoryGameManager _memoryGameManager;
        private CanvasHolder _canvasHolder;
        private SaveLoadManager _saveLoadManager;
        private Hud _hud;

        #endregion

        private void Awake()
        {
            _camera = Instantiate(_gameConfig.Camera);
            _camera.name = _camera.name.Declone();
            
            InitInputManager();
            
            Instantiate(_gameConfig.AudioManager, transform);
            
            InitSaveLoadSystem();
            InitUi();
            InitMemoryGameManager();
        }

        private void InitSaveLoadSystem()
        {
            _saveLoadManager = Instantiate(_gameConfig.SaveLoadManager, transform);
            _saveLoadManager.name = _saveLoadManager.name.Declone();
        }

        private void InitUi()
        {
            _canvasHolder = Instantiate(_gameConfig.CanvasHolder);
            _canvasHolder.Init(_camera);
            _canvasHolder.name = _canvasHolder.name.Declone(); 
            
            var gameUi = Instantiate(_gameConfig.GameUi, _canvasHolder.transform);
            gameUi.Init(_saveLoadManager, _inputManager);
            gameUi.name = gameUi.name.Declone(); 
            
            _hud = Instantiate(_gameConfig.Hud, _canvasHolder.transform);
            _hud.name = _hud.name.Declone();
        }

        private void InitMemoryGameManager()
        {
            _memoryGameManager = Instantiate(_gameConfig.MemoryGameManager);
            _memoryGameManager.Init(_camera, _hud);
        }

        private void InitInputManager()
        {
            _inputManager = Instantiate(_gameConfig.InputManager, transform);
            _inputManager.Init(_camera);
        }
    }
}