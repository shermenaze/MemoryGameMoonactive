using CardGame.SaveSystem;
using CardGame.UI;
using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(menuName = "GameConfigs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        #region Fields

        [SerializeField] private InputManager _inputManager;
        [SerializeField] private SaveLoadManager _saveLoadManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private MemoryGameManager _memoryGameManager;
        [SerializeField] private Camera _camera;
        [SerializeField] private CanvasHolder _canvasHolder;
        [SerializeField] private GameUI _gameUi;
        [SerializeField] private Hud _hud;

        #endregion

        #region Properties

        public InputManager InputManager => _inputManager;
        public SaveLoadManager SaveLoadManager => _saveLoadManager;
        public AudioManager AudioManager => _audioManager;
        public Camera Camera => _camera;
        public MemoryGameManager MemoryGameManager => _memoryGameManager;
        public CanvasHolder CanvasHolder => _canvasHolder;
        public GameUI GameUi => _gameUi;
        public Hud Hud => _hud;

        #endregion
    }   
}