using CardGame.SaveSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class GameUI : MonoBehaviour
    {
        #region Fields

        [Header("Screens")]
        [SerializeField] private RectTransform _uiElements;
        [SerializeField] private RectTransform _screensParent;
        [SerializeField] private RectTransform _welcomeMenu;
        [SerializeField] private RectTransform _pauseMenu;
        
        [Header("Animations")]
        [SerializeField] [Range(0, 2)] private int _screenMoveDuration = 1;
        [SerializeField] [Range(0, 2)] private float _menuScaleDuration = 0.4f;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private HeaderMessageSO _welcomeHeaderMessageSo;

        [Header("Loadable Elements")]
        [SerializeField] private Slider _volumeLevel;
        [SerializeField] private Toggle _muteToggle;
        [SerializeField] private TMP_InputField _playerNameInput;

        private RectTransform _currentMenu;
        private RectTransform _lastMenu;
        private HeaderMessageSO _lastMessage;
        private bool _isEnabled;

        #endregion
        
        private void Awake()
        {
            SetHeader(_welcomeHeaderMessageSo);
            _lastMessage = _welcomeHeaderMessageSo;
            _currentMenu = _welcomeMenu;
            _isEnabled = true;
            ScaleUi();
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
            //if(menu == _currentMenu) return;
            menu.gameObject.SetActive(true);
            
            var newPosition = _screensParent.localPosition.y > 0 ? Vector2.zero : new Vector2(0, 1080);
            
            _screensParent.DOLocalMove(newPosition, _isEnabled ? 0 : _screenMoveDuration)
                .SetEase(Ease.InOutQuint)
                .SetUpdate(true)
                .OnComplete(()=>
                {
                    _lastMenu = _currentMenu;
                    _currentMenu.gameObject.SetActive(false);
                    _currentMenu = menu;
                });
        }

        /// <summary>
        /// Show the previous menu
        /// </summary>
        public void ShowLastMenu()
        {
            ShowMenu(_lastMenu);
            SetHeader(_lastMessage);
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
            if(gameState != null && _muteToggle) gameState.Mute = _muteToggle.isOn;
            if(gameState != null && _volumeLevel) gameState.MusicVolume = _volumeLevel.value;
            if(gameState != null && _playerNameInput) gameState.PlayerName = _playerNameInput.text;
        }
        
        /// <summary>
        /// Load all loadable elements from the incoming GameState
        /// </summary>
        /// <param name="gameState">Loaded GameState</param>
        public void LoadGame(GameState gameState)
        {
            if(gameState != null && _muteToggle) _muteToggle.isOn = gameState.Mute;
            if(gameState != null && _volumeLevel) _volumeLevel.value = gameState.MusicVolume;
            if(gameState != null && _playerNameInput) _playerNameInput.text = gameState.PlayerName;
        }
    }
}