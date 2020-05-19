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
        [SerializeField] private ParticleSystem _particleSystem;

        private MemoryGameManager _memoryGameManager;
        private CardSO _cardData;
        private int _cardNumber;
        private bool _isFaceUp = false;
        private Sprite _cardSprite;
        private Collider2D _collider;
        private Vector3 _endRotation;

        #endregion

        #region Properties

        private bool Enabled { set => _collider.enabled = value; }
        public int CardNumber => _cardNumber;
        public bool FaceUp => _isFaceUp;

        public ParticleSystem Particles => _particleSystem;
        
        #endregion

        private void Awake()
        {
            _endRotation = new Vector3(0, 180, 0);
            _collider = GetComponent<Collider2D>();
            _memoryGameManager = GetComponentInParent<MemoryGameManager>();
        }
        
        public void Init(CardSO cardData, int cardNumber, int sortingOrder)
        {
            _cardData = cardData;
            name = _cardData.name;

            _cardNumber = cardNumber;
            _cardSprite = _cardData.CardSprite;
            
            _cardSpriteRenderer.sortingOrder += sortingOrder;
            _frameSpriteRenderer.sortingOrder += sortingOrder;
        }

        /// <summary>
        /// Moves this Card to a goal position
        /// </summary>
        /// <param name="endPosition">The end position</param>
        /// <param name="animationDelay">Delay before animation begins</param>
        public void Animate(Vector3 endPosition, float animationDelay)
        {
            transform.DOMove(endPosition, 1).SetDelay(animationDelay);
        }

        /// <summary>
        /// The Card was clicked; Check match to previously clicked Card
        /// </summary>
        public void Click()
        {
            _memoryGameManager.CheckMatch(this);
        }

        /// <summary>
        /// Rotates card to face the other way
        /// </summary>
        /// <param name="enabledWhenDone">Should this Card's Collider be enabled after rotation</param>
        /// <param name="delay">Delay before rotation</param>
        public void RotateCard(bool enabledWhenDone, float delay = 0)
        {
            Enabled = false;

            if(AudioManager.Instance.isActiveAndEnabled)
                AudioManager.Instance.PlaySound(_cardFlipAudio, 0.3f);

            transform.DORotate(_endRotation, 0.4f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuint)
                .SetUpdate(true)
                .SetDelay(delay)
                .OnUpdate(SwitchCardSprite)
                .OnComplete(() =>
                {
                    _isFaceUp = !_isFaceUp;
                    Enabled = enabledWhenDone;
                });
        }

        /// <summary>
        /// If this Card is facing up, rotates it, sets collider enabled to false
        /// </summary>
        public void ResetCard()
        {
            if (_isFaceUp) RotateCard(false, 0.4f);
            Enabled = false;
        }
        
        /// <summary>
        /// Switch this Card's sprite according to its rotation
        /// </summary>
        private void SwitchCardSprite()
        {
            if (transform.localRotation.y < 0.4f || transform.localRotation.y > 0.9f) return;
            _cardSpriteRenderer.sprite = transform.localRotation.y > 0.7f ? _cardSprite : _backSprite;
        }
    }
}