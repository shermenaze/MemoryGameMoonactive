using CardGame.SaveSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame.UI
{
    public class GameUI : MonoBehaviour
    {
        #region Fields

        [Header("Screens")] [SerializeField] private RectTransform _uiElements;
        [SerializeField] private RectTransform _screensParent;
        [SerializeField] private RectTransform _welcomeMenu;
        [SerializeField] private RectTransform _pauseMenu;

        [Header("Animations")] [SerializeField] [Range(0, 2)]
        private int _screenMoveDuration = 1;

        [SerializeField] [Range(0, 2)] private float _menuScaleDuration = 0.4f;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private HeaderMessageSO _welcomeHeaderMessageSo;

        [Header("Buttons")] [SerializeField] private Button _startButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;

        [Header("Loadable Elements")] [SerializeField]
        private Slider _volumeLevel;

        [SerializeField] private Toggle _muteToggle;
        [SerializeField] private TMP_InputField _playerNameInput;

        private RectTransform _currentMenu;
        private RectTransform _previousMenu;
        private string _lastMessage;
        private bool _isEnabled;
        private SaveLoadManager _saveLoadManager;

        #endregion

        private void Awake()
        {
            SetHeader(_welcomeHeaderMessageSo);
            _lastMessage = _welcomeHeaderMessageSo.Message;
            _currentMenu = _welcomeMenu;
            _isEnabled = true;
            ScaleUi();
        }

        public void Init(SaveLoadManager saveLoadManager)
        {
            _saveLoadManager = saveLoadManager;
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
        }

        /// <summary>
        /// Sets the UI header text according to the menu type provided
        /// </summary>
        /// <param name="headerMessageSo">Header message object</param>
        public void SetHeader(HeaderMessageSO headerMessageSo)
        {
            _headerText.text = headerMessageSo.Message;
        }

        /// <summary>
        /// Enables the incoming menu parameter, and animate it into view 
        /// </summary>
        /// <param name="menu">The menu to show</param>
        public void ShowMenu(RectTransform menu)
        {
            //if (_previousMenu == _currentMenu) return;
            _previousMenu = _currentMenu;

            menu.gameObject.SetActive(true);
            
            var newPosition = _screensParent.localPosition.y > 0 ? Vector2.zero : new Vector2(0, 1080);

            _screensParent.DOLocalMove(newPosition, _isEnabled ? 0 : _screenMoveDuration)
                .SetEase(Ease.InOutQuint)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    _currentMenu.gameObject.SetActive(false);
                    _currentMenu = menu;
                });
        }

        /// <summary>
        /// Show the previous menu
        /// </summary>
        public void ShowLastMenu()
        {
            ShowMenu(_previousMenu);
            _lastMessage = _headerText.text;
        }

        /// <summary>
        /// Scales the UI according to _isEnabled
        /// </summary>
        public void ScaleUi()
        {
            if (_isEnabled)
            {
                Time.timeScale = 0;

                _currentMenu.gameObject.SetActive(true);
                _uiElements.DOScale(Vector3.one, _menuScaleDuration).SetEase(Ease.OutQuint).SetUpdate(true);

                _isEnabled = false;
            }
            else
            {
                Time.timeScale = 1;
                _uiElements.DOScale(Vector3.zero, _menuScaleDuration).SetEase(Ease.InQuint)
                    .OnComplete(() =>
                    {
                        _screensParent.localPosition = Vector2.zero;
                        _currentMenu.gameObject.SetActive(false);
                        _currentMenu = _pauseMenu;
                    });

                _isEnabled = true;
            }
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
    }
}