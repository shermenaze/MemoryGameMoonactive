using DG.Tweening;
using UnityEngine;

namespace CardGame
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _uiElements;
        [SerializeField] private RectTransform _settingsMover;

        private void Awake()
        {
            _uiElements.localScale = Vector3.zero;
        }

        public void MoveSettingsElements()
        {
            var newPosition = _settingsMover.localPosition.y > 0 ? Vector2.zero : new Vector2(0, 1080);
            _settingsMover.DOLocalMove(newPosition, 1).SetEase(Ease.InOutQuint).SetUpdate(true);
        }
        
        public void EnableUI(bool enabled)
        {
            if (!enabled) return;

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