using System;
using System.Collections;
using UnityEngine;

namespace CardGame
{
    public class CardRotation : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float Speed;
        [SerializeField] private CardSO _cardData;
        [SerializeField] private Sprite _backSprite;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Quaternion _backRotation;
        private Quaternion _frontRotation;
        private float _flipCondition;

        private const float RotateToFrontCondition = 0f;
        private const float RotateToBackCondition = -1f;

        #endregion

        private void Awake()
        {
            _backRotation = Quaternion.Euler(0, 0, 0);
            _frontRotation = Quaternion.Euler(0, 180, 0);
        }

        private void Rotate()
        {
            StopAllCoroutines();

            _flipCondition = -0.7f;
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
                    ? Quaternion.Slerp(rotation, _frontRotation, Time.deltaTime * Speed)
                    : Quaternion.Slerp(rotation, _backRotation, Time.deltaTime * Speed);

                transform.rotation = rotation;

                _spriteRenderer.sprite = rotation.y <= _flipCondition ? _cardData.CardSprite : _backSprite;
                yield return null;
            }
        }
    }
}