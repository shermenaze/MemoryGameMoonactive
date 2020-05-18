using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CardGame.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counterText;
        [SerializeField] private TextMeshProUGUI _gameEndText;
        [SerializeField] private GameEvent _gameLostEvent;

        public float Counter
        {
            get => _counter;
            set => _counter = value;
        }

        private float _counter = 30;
        private bool _isGameOn;

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
                _counterText.transform.DOScale(Vector3.zero, 0.3f).OnComplete(()=>
                {
                    _gameEndText.text = "You Lost!";
                    _gameEndText.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic);
                });
                
                _isGameOn = false;
            }

            if (_counter <= 6) _counterText.color = Color.red;
            else if (_counter <= 15) _counterText.color = Color.yellow;

            _counter -= Time.deltaTime;
            _counterText.text = _counter.ToString("F1");
        }

        public void StartCounter()
        {
            _counterText.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutElastic);
            _isGameOn = true;
        }
    }
}