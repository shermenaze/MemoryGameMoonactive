using DG.Tweening;
using UnityEngine;

namespace CardGame
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _uiElements;
        [SerializeField] private RectTransform _settingsMover;
        [SerializeField] private RectTransform _mainMenu;
        [SerializeField] [Range(0, 2)] private int _moveDuration = 1;

        private RectTransform _currentMenu;

        private void Awake()
        {
            _uiElements.localScale = Vector3.zero;
            _currentMenu = _mainMenu;
        }

        public void ShowMenuElements(RectTransform menu)
        {
            menu.gameObject.SetActive(true);
            
            var newPosition = _settingsMover.localPosition.y > 0 ? Vector2.zero : new Vector2(0, 1080);
            
            _settingsMover.DOLocalMove(newPosition, _moveDuration).SetEase(Ease.InOutQuint).SetUpdate(true)
                .OnComplete(()=>
                {
                    _currentMenu.gameObject.SetActive(false);
                    _currentMenu = menu;
                });
        }
        
        public void EnableUI(bool enabled)
        {
            if (!enabled) return;

            _currentMenu = _mainMenu;
            _currentMenu.gameObject.SetActive(true);
            
            _uiElements.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutQuint)
                .OnComplete(() => Time.timeScale = 0);
        }

        public void DisableUI(bool enabled)
        {
            if (enabled) return;
            
            Time.timeScale = 1;
            _uiElements.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InQuint)
                .OnComplete(()=>_settingsMover.localPosition = Vector2.zero);
        }
    }
}