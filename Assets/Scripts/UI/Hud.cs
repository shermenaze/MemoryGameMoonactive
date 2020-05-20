using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CardGame.UI
{
    public class Hud : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TextMeshProUGUI _counterText;
        [SerializeField] private GameEvent _gameLostEvent;
        [SerializeField] private float _counter = 30;

        private float initialCounter;
        private bool _isGameOn;

        #endregion

        private void Awake()
        {
            initialCounter = _counter;
        }

        private void Update()
        {
            if (_isGameOn) Count();
        }

        /// <summary>
        /// Counts down to 0, if under 0, game lost
        /// </summary>
        private void Count()
        {
            if (_counter <= 0)
            {
                _counterText.text = 0.ToString();
                RestartGame();
                _gameLostEvent.Raise();

                _counterText.transform.DOScale(Vector3.zero, 0.3f);
                
                _isGameOn = false;
            }

            if (_counter <= 6) ChangeCounterTextColor(Color.red);
            else if (_counter <= 15) ChangeCounterTextColor(Color.yellow);

            _counter -= Time.deltaTime;
            _counterText.text = _counter.ToString("F0");
        }

        private void ChangeCounterTextColor(Color color)
        {
            _counterText.color = color;
        }

        public void RestartGame()
        {
            _isGameOn = false;
            _counterText.transform.DOScale(Vector3.zero, 0.3f).SetUpdate(true)
                .OnComplete(()=>
                {
                    ChangeCounterTextColor(Color.white);
                    _counter = initialCounter;
                });
        }
        
        public void StartCounter()
        {
            _counterText.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic);
            _isGameOn = true;
        }
    }
}