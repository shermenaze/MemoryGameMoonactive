using DG.Tweening;
using UnityEngine;

namespace CardGame
{
    [RequireComponent(typeof(Collider2D))]
    public class Card : MonoBehaviour, IClickable
    {
        #region Fields
        
        [SerializeField] private Sprite _backSprite;
        [SerializeField] private SpriteRenderer _cardSpriteRenderer;
        [SerializeField] private SpriteRenderer _frameSpriteRenderer;
        [SerializeField] private AudioClip _cardFlipAudio;

        private CardsGameManager _cardsGameManager;
        private CardSO _cardData;
        private int _cardNumber;
        private Sprite _cardSprite;
        private Collider2D _collider;

        #endregion

        #region Properties

        private bool Enabled { set => _collider.enabled = value; }
        public int CardNumber => _cardNumber;

        #endregion

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public void Init(CardsGameManager cardsGameManager, CardSO cardData, int cardNumber, int sortingOrder)
        {
            _cardsGameManager = cardsGameManager;
            
            _cardData = cardData;
            name = _cardData.name;

            _cardNumber = cardNumber;
            _cardSprite = _cardData.CardSprite;
            
            _cardSpriteRenderer.sortingOrder += sortingOrder;
            _frameSpriteRenderer.sortingOrder += sortingOrder;
        }

        public void Animate(Vector3 position, float animationDelay)
        {
            transform.DOMove(position, 1).SetDelay(animationDelay);
        }

        public void Click()
        {
            _cardsGameManager.CheckMatch(this);
        }

        public void RotateCard(bool enabledWhenDone, float delay = 0)
        {
            Enabled = false;

            if(AudioManager.Instance.isActiveAndEnabled)
                AudioManager.Instance.PlaySound(_cardFlipAudio, 0.3f);

            var endRotation = new Vector3(0, 180, 0);

            transform.DORotate(endRotation, 0.4f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuint)
                .SetDelay(delay)
                .OnUpdate(SwitchCardSprite)
                .OnComplete(() => Enabled = enabledWhenDone);
        }

        private void SwitchCardSprite()
        {
            if (transform.localRotation.y < 0.4f || transform.localRotation.y > 0.9f) return;
            _cardSpriteRenderer.sprite = transform.localRotation.y > 0.7f ? _cardSprite : _backSprite;
        }
    }
}