using System;
using System.Collections;
using UnityEngine;

namespace CardGame
{
    public class Card : MonoBehaviour, IClickable
    {
        #region Fields

        [SerializeField][Range(0.5f, 5)] private float _speed = 3f;
        [SerializeField] private Sprite _backSprite;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private CardsManager _cardsManager;
        private CardSO _cardData;
        private int _cardNumber;
        private Sprite _cardSprite;
        private Quaternion _backRotation;
        private Quaternion _frontRotation;

        private const float _flipCondition = -0.7f;
        private const float RotateToFrontCondition = 0f;
        private const float RotateToBackCondition = -1f;

        #endregion

        private void Awake()
        {
            _backRotation = Quaternion.Euler(0, 0, 0);
            _frontRotation = Quaternion.Euler(0, 180, 0);
        }

        public void Init(CardsManager cardsManager, CardSO cardData, int cardNumber)
        {
            _cardsManager = cardsManager;
            _cardData = cardData; //TODO: Remove cach, just take sprite

            name = _cardData.name;
            _cardNumber = cardNumber;
            _cardSprite = _cardData.CardSprite;
        }
        
        public void Clicked()
        {
            StopAllCoroutines();

            var rotationCondition = transform.rotation.y > _flipCondition;
            StartCoroutine(RotateCard(rotationCondition));
        }

        private IEnumerator RotateCard(bool rotateToFront)
        {
            var newRotation = rotateToFront ? RotateToBackCondition : RotateToFrontCondition;
            var rotation = transform.rotation;

            while (Math.Abs(rotation.y - newRotation) > float.Epsilon)
            {
                rotation = rotateToFront
                    ? Quaternion.Slerp(rotation, _frontRotation, Time.deltaTime * _speed)
                    : Quaternion.Slerp(rotation, _backRotation, Time.deltaTime * _speed);

                transform.rotation = rotation;

                _spriteRenderer.sprite = rotation.y <= _flipCondition ? _cardSprite : _backSprite;
                yield return null;
            }
        }
    }
}