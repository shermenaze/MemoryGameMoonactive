using CardGame.SaveSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardGame.UI
{
    public class GameUI : MonoBehaviour
    {
        #region Fields

        [Header("Screens")] [SerializeField] private RectTransform _uiElements;
        [SerializeField] private RectTransform _screensParent;
        [SerializeField] private UiScreen _welcomeScreen;
        [SerializeField] private UiScreen _pauseScreen;

        [Header("Animations")] [SerializeField]
        private TextMeshProUGUI _headerText;

        [SerializeField] [Range(0, 2)] private int _screenMoveDuration = 1;
        [SerializeField] [Range(0, 2)] private float _menuScaleDuration = 0.4f;

        [Header("Buttons")] [SerializeField] private Button _startButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;

        [Header("Loadable Elements")] [SerializeField]
        private Slider _volumeLevel;

        [SerializeField] private Toggle _muteToggle;
        [SerializeField] private TMP_InputField _playerNameInput;

        private UiScreen _currentScreen;
        private UiScreen _previousScreen;
        private SaveLoadManager _saveLoadManager;
        private InputManager _inputManager;
        private Button _currentClickedButton;
        private Button _previousClickedButton;
        private EventSystem _eventSystem;
        private bool _isEnabled;

        #endregion

        private void Awake()
        {
            SetHeader(_welcomeScreen.MessageSo);
            _currentScreen = _welcomeScreen;
            _isEnabled = true;
            ScaleUi();
        }

        private void Start() => _eventSystem = EventSystem.current;

        public void Init(SaveLoadManager saveLoadManager, InputManager inputManager)
        {
            _saveLoadManager = saveLoadManager;
            _inputManager = inputManager;
            AddListeners();
        }

        private void AddListeners()
        {
            AudioManager.Instance.AudioSource.volume = _volumeLevel.value;
            _volumeLevel.onValueChanged.AddListener(value => AudioManager.Instance.AudioSource.volume = value);

            AudioManager.Instance.AudioSource.mute = _muteToggle.isOn;
            _muteToggle.onValueChanged.AddListener(isOn => AudioManager.Instance.AudioSource.mute = isOn);

            _saveButton.onClick.AddListener(_saveLoadManager.Save);
            _loadButton.onClick.AddListener(_saveLoadManager.Load);
            _startButton.onClick.AddListener(() => _inputManager.enabled = true);
        }

        /// <summary>
        /// Sets the UI header text according to the menu type provided
        /// </summary>
        /// <param name="headerMessageSo">Header message object</param>
        /// <param name="endvalue">The end value</param>
        private void SetHeader(HeaderMessageSO headerMessageSo, float endValue = 1)
        {
            _headerText.text = headerMessageSo.Message;
            _headerText.DOFade(endValue, _screenMoveDuration * 0.5f).SetUpdate(true);
        }

        /// <summary>
        /// Enables the incoming menu parameter, and animate it into view 
        /// </summary>
        /// <param name="menu">The menu to show</param>
        public void ShowMenu(UiScreen menu)
        {
            EnableInput(false);

            _headerText.DOFade(0, _screenMoveDuration * 0.5f).SetUpdate(true)
                .OnComplete(() => SetHeader(menu.MessageSo));

            _previousScreen = _currentScreen;
            _currentScreen = menu;

            menu.gameObject.SetActive(true);

            var newPosition = _screensParent.localPosition.y > 0 ? Vector2.zero : new Vector2(0, 1080);

            _screensParent.DOLocalMove(newPosition, _isEnabled ? 0 : _screenMoveDuration)
                .SetEase(Ease.InOutQuint)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    _previousScreen.gameObject.SetActive(false);
                    EnableInput(true);
                });
        }

        /// <summary>
        /// Enable or disable input while ui is animating
        /// </summary>
        /// <param name="enable"></param>
        private void EnableInput(bool enable)
        {
            if (_currentScreen != _welcomeScreen && _previousScreen != _welcomeScreen)
                _inputManager.enabled = enable;

            _eventSystem.enabled = enable;
        }

        /// <summary>
        /// Show the previous menu
        /// </summary>
        public void ShowLastMenu()
        {
            ShowMenu(_previousScreen);
        }

        /// <summary>
        /// Scales the UI according to _isEnabled
        /// </summary>
        public void ScaleUi()
        {
            if (_isEnabled)
            {
                Time.timeScale = 0;

                _currentScreen.gameObject.SetActive(true);
                _uiElements.DOScale(Vector3.one, _menuScaleDuration).SetEase(Ease.OutQuint).SetUpdate(true)
                    .OnComplete(() => _isEnabled = false);
            }
            else
            {
                Time.timeScale = 1;
                _uiElements.DOScale(Vector3.zero, _menuScaleDuration)
                    .SetEase(Ease.InQuint).OnComplete(ScaleDownMenu);
            }
        }

        private void ScaleDownMenu()
        {
            _screensParent.localPosition = Vector2.zero;
            _currentScreen.gameObject.SetActive(false);

            
            _currentScreen = _pauseScreen;
            SetHeader(_pauseScreen.MessageSo, 0);
            _isEnabled = true;
        }

        /// <summary>
        /// Save all loadable elements into the incoming GameState
        /// </summary>
        /// <param name="gameState">new GameState</param>
        public void SaveGame(GameState gameState)
        {
            if (gameState == null) return;

            if (_muteToggle) gameState.Mute = _muteToggle.isOn;
            if (_volumeLevel) gameState.MusicVolume = _volumeLevel.value;
            if (_playerNameInput) gameState.PlayerName = _playerNameInput.text;
        }

        /// <summary>
        /// Load all loadable elements from the incoming GameState
        /// </summary>
        /// <param name="gameState">Loaded GameState</param>
        public void LoadGame(GameState gameState)
        {
            if (gameState == null) return;

            if (_muteToggle) _muteToggle.isOn = gameState.Mute;
            if (_volumeLevel) _volumeLevel.value = gameState.MusicVolume;
            if (_playerNameInput) _playerNameInput.text = gameState.PlayerName;
        }

        private void OnDisable()
        {
            _muteToggle.onValueChanged.RemoveAllListeners();
            _volumeLevel.onValueChanged.RemoveAllListeners();
            _saveButton.onClick.RemoveAllListeners();
            _loadButton.onClick.RemoveAllListeners();
            _startButton.onClick.RemoveAllListeners();
        }
    }
}