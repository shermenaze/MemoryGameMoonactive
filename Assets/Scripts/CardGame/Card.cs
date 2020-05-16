using DG.Tweening;
using UnityEngine;

namespace CardGame
{
    [RequireComponent(typeof(Collider2D))]
    public class Card : MonoBehaviour, IClickable
    {
        #region Fields

        [SerializeField] [Range(0.5f, 5)] private float _speed = 3f;
        [SerializeField] private Sprite _backSprite;
        [SerializeField] private SpriteRenderer _cardSpriteRenderer;
        [SerializeField] private SpriteRenderer _frameSpriteRenderer;

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
            _cardData = cardData; //TODO: Remove cach, just take sprite

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

        public void Clicked()
        {
            _cardsGameManager.CheckMatch(this);
        }

        public void RotateCard(bool enabledWhenDone, float delay = 0)
        {
            Enabled = false;

            //AudioManager.Instance.PlaySound(isHidden ? _buttonFlipOpenClip : _buttonFlipCloseClip, 0.3f);

            var endRotation = new Vector3(0, 180, 0);

            transform.DORotate(endRotation, 0.4f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuint)
                .SetDelay(delay)
                .OnUpdate(SwitchCardSprite)
                .OnComplete(() =>
                {
                    Enabled = enabledWhenDone;
                    Debug.Log(_collider.enabled);
                });
        }

        private void SwitchCardSprite()
        {
            _cardSpriteRenderer.sprite = transform.localRotation.y > 0.7f ? _cardSprite : _backSprite;
        }

        // private IEnumerator InitialRotation(float waitForAnimIn)
        // {
        //     yield return new WaitForSeconds(waitForAnimIn);
        //     _animate.AnimIn();
        //
        //     yield return new WaitForSeconds(4);
        //     RotateCardDo(true);
        // }
    }
}