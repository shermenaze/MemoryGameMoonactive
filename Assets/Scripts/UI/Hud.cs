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
        
        private float _counter = 30;
        private bool _isGameOn;

        #endregion

        public float Counter
        {
            get => _counter;
            set => _counter = value;
        }

        private void Update()
        {
            if (_isGameOn) Count();
        }

        private void Count()
        {
            if (_counter <= 0)
            {
                _counterText.text = 0.ToString();
                _gameLostEvent.Raise();

                _counterText.transform.DOScale(Vector3.zero, 0.3f);
                
                _isGameOn = false;
            }

            if (_counter <= 6) _counterText.color = Color.red;
            else if (_counter <= 15) _counterText.color = Color.yellow;

            _counter -= Time.deltaTime;
            _counterText.text = _counter.ToString("F0");
        }

        public void RestartGame()//TODO: Restart game event
        {
            _isGameOn = false;
            _counterText.transform.DOScale(Vector3.zero, 0.3f).SetUpdate(true)
                .OnComplete(()=>
                {
                    _counterText.color = Color.white;
                    _counter = 30;
                });
        }
        
        public void StartCounter()
        {
            _counterText.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic);
            _isGameOn = true;
        }
    }
}