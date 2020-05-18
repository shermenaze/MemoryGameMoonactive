using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class CardsSpawner
    {
        #region Fields

        private readonly Camera _camera;
        private readonly Card _cardPrefab;
        private readonly Transform _instantiatePosition;
        private readonly CardSO[] _cardsSo;
        private readonly Transform _deckPosition;

        private readonly int _cardsAmount;
        private float _cardsScale;
        private readonly int _columns;
        private readonly int _rows;
        private readonly float _initialDelay;

        #endregion
        
        public CardsSpawner(BoardSo _boardData, Camera camera, Card cardPrefab,
            Transform initialPosition, Transform deckPosition, CardSO[] cardsSo, float initialDelay)
        {
            _initialDelay = initialDelay;
            _deckPosition = deckPosition;
            _cardsSo = cardsSo;
            _instantiatePosition = initialPosition;
            _cardPrefab = cardPrefab;
            _camera = camera;
            _columns = _boardData.Columns;
            _rows = _boardData.Rows;
            _cardsAmount = _columns * _rows;
        }
        
        public Vector3[] InitCardsPositions()
        {
            var positions = new Vector3[_cardsAmount];
            int vectorsEntered = 0;
            var negativeCameraAspect = _camera.aspect * _camera.orthographicSize * -1f;
            var xOffset = Mathf.Abs(negativeCameraAspect) * 0.5f;
            var yOffset = Mathf.Abs(negativeCameraAspect) * 0.6f;

            _cardsScale = xOffset * 0.65f;

            Vector3 newPosition = Vector3.zero;
            newPosition.x = negativeCameraAspect + xOffset * 0.5f;
            newPosition.y = negativeCameraAspect + yOffset * 0.5f - _camera.aspect * (_columns - 2);

            for (int j = 0; j < _columns; j++)
            {
                if (j != 0) newPosition.y += yOffset;

                for (int i = 0; i < _rows; i++)
                {
                    if (i != 0) newPosition.x += xOffset;
                    positions[vectorsEntered++] = newPosition;
                }

                newPosition.x = negativeCameraAspect + xOffset * 0.5f;
            }

            return positions;
        }

        public List<Card> InitCards()
        {
            var usedNumbers = new List<int>();
            float animationDelay = 0;
            int orderInLayer = 0;
            var cardsList = new List<Card>();
            
            for (int cardIndex = 0; cardIndex < _cardsAmount; cardIndex++)
            {
                var card = GameObject.Instantiate(_cardPrefab, _instantiatePosition);
                card.transform.localScale *= _cardsScale;
                var randomNumber = GetRandomNumber(_cardsSo, usedNumbers);
                var cardSo = _cardsSo[randomNumber];

                card.Init(cardSo, randomNumber, orderInLayer += 2);
                cardsList.Add(card);
                card.Animate(_deckPosition.position, animationDelay);
                animationDelay += _initialDelay;
            }

            return cardsList;
        }
        
        private static int GetRandomNumber(IReadOnlyCollection<CardSO> cards, List<int> usedNumbers)
        {
            int GetRandom() => Random.Range(0, cards.Count);

            var randomNumber = GetRandom();
            var numberExists = usedNumbers.Exists(n => n == randomNumber);

            while (numberExists)
            {
                randomNumber = GetRandom();
                numberExists = usedNumbers.Exists(x => x == randomNumber);
            }

            usedNumbers.Add(randomNumber);

            if (usedNumbers.Count == cards.Count) usedNumbers.Clear();

            return randomNumber;
        }
    }
}