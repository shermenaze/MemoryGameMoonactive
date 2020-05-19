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
        private List<Card> _cardsList;

        #endregion
        
        public CardsSpawner(MemoryGameManager memoryGameManager)
        {
            _camera = memoryGameManager.CurrentCamera;
            
            _instantiatePosition = memoryGameManager.InitialTransform;
            _deckPosition = memoryGameManager.DeckTransform;
            
            _cardsSo = memoryGameManager.CardsSo;
            _cardPrefab = memoryGameManager.CardPrefab;

            _initialDelay = memoryGameManager.InitialDelay;
            
            _columns = memoryGameManager.BoardData.Columns;
            _rows = memoryGameManager.BoardData.Rows;
            _cardsAmount = _columns * _rows;
        }
        
        /// <summary>
        /// Calculates camera's aspect ratio to initiate an array of columns by rows Vector3 positions
        /// </summary>
        /// <returns>A pre calculated array of Vector3 positions</returns>
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

        /// <summary>
        /// Instantiates Cards and animate them towards their initial position
        /// </summary>
        /// <returns>A list of initialized Cards</returns>
        public List<Card> InitCards()
        {
            var usedNumbers = new List<int>();
            int orderInLayer = 0;
            _cardsList = new List<Card>();
            
            for (int cardIndex = 0; cardIndex < _cardsAmount; cardIndex++)
                InstantiateCard(usedNumbers, ref orderInLayer);

            AnimateCards(_cardsList, _deckPosition.position);

            return _cardsList;
        }

        /// <summary>
        /// Instantiate a card, scale it according to camera size,
        /// assign it a random number index,
        /// Initialize it and increment order in layer by 2
        /// </summary>
        /// <param name="usedNumbers">A list of numbers used to index other cards</param>
        /// <param name="orderInLayer">The order in layer for the current Card</param>
        private void InstantiateCard(List<int> usedNumbers, ref int orderInLayer)
        {
            var card = Object.Instantiate(_cardPrefab, _instantiatePosition);
            card.transform.localScale *= _cardsScale;

            var randomNumber = GetRandomNumber(_cardsSo, usedNumbers);
            var cardSo = _cardsSo[randomNumber];

            card.Init(cardSo, randomNumber, orderInLayer += 2);
            _cardsList.Add(card);
        }

        public void RestartGame()
        {
            _cardsList.ForEach(card =>
            {
                card.ResetCard();
                card.transform.localPosition = _instantiatePosition.position;
            });
            
            AnimateCards(_cardsList, _deckPosition.position);
        }
        
        /// <summary>
        /// Animate all cards with a delay from current position towards EndPosition
        /// </summary>
        /// <param name="cards">A List of cards</param>
        /// <param name="EndPosition">The goal position</param>
        private void AnimateCards(List<Card> cards, Vector3 EndPosition)
        {
            float animationDelay = 0;
            cards.ForEach(card => card.Animate(EndPosition, animationDelay += _initialDelay));
        }

        /// <summary>
        /// Gets a random number that was not previously chosen
        /// </summary>
        /// <param name="cards">A list of cards</param>
        /// <param name="usedNumbers">A list for used random numbers</param>
        /// <returns>New random number</returns>
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