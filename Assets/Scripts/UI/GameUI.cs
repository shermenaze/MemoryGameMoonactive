using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CardGame
{
    public class GameUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private RectTransform _uiElements;
        [SerializeField] private RectTransform _settingsMover;
        [SerializeField] private RectTransform _welcomeMenu;
        [SerializeField] private RectTransform _pauseMenu;
        [SerializeField] [Range(0, 2)] private int _moveDuration = 1;
        [SerializeField] [Range(0, 2)] private float _menuScaleDuration = 0.4f;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private HeaderMessageSO _welcomeHeaderMessageSo;

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
            ScaleUI();
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
            menu.gameObject.SetActive(true);
            
            var newPosition = _settingsMover.localPosition.y > 0 ? Vector2.zero : new Vector2(0, 1080);
            
            _settingsMover.DOLocalMove(newPosition, _isEnabled ? 0 : _moveDuration)
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
        public void ScaleUI()
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
                        _settingsMover.localPosition = Vector2.zero;
                        _currentMenu.gameObject.SetActive(false);
                        _currentMenu = _pauseMenu;
                    });
                
                _isEnabled = true;
            }
        }
    }
}