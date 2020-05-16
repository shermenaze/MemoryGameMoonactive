using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardGame
{
    public class CardsManager : MonoBehaviour
    {
        [SerializeField] private Transform _instantiatePosition;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private CardSO[] _cardsSo;
        [SerializeField] private BoardSo _boardData; //TODO: Add functionality or delete

        [SerializeField] private Camera _camera; //TODO: Move with DivideScreenRealEstate function

        private readonly List<Card> _cardsList = new List<Card>();
        private Vector3[] _cardsPositions;
        private float _cardsScale;

        private const int Rows = 4;
        private const int Columns = 4;
        private const int CardsAmount = Rows * Columns;

        private void Start()
        {
            InitCardsPositions();
            InitCards();
        }

        private void InitCards()
        {
            var usedNumbers = new List<int>();

            for (int cardIndex = 0; cardIndex < CardsAmount; cardIndex++)
            {
                var card = Instantiate(_cardPrefab, _instantiatePosition);
                card.transform.localScale *= _cardsScale;
                var randomNumber = GetRandomNumber(_cardsSo, usedNumbers);
                var cardSo = _cardsSo[randomNumber];

                card.Init(this, cardSo, randomNumber);
                _cardsList.Add(card);
            }
        }

        private void InitCardsPositions()
        {
            var positions = new Vector3[CardsAmount];
            int vectorsEntered = 0;

            var negativeCameraAspect = _camera.aspect * _camera.orthographicSize * -1f;
            var xOffset = Mathf.Abs(negativeCameraAspect) * 0.5f;
            var yOffset = Mathf.Abs(negativeCameraAspect) * 0.6f;
            
            _cardsScale = xOffset * 0.65f;

            Vector3 newPosition = Vector3.zero;
            newPosition.x = negativeCameraAspect + xOffset * 0.5f;
            newPosition.y = negativeCameraAspect + yOffset * 0.5f - _camera.aspect * 3.5f; //TODO: Not like this

            for (int j = 0; j < Columns; j++)
            {
                if (j != 0) newPosition.y += yOffset;

                for (int i = 0; i < Rows; i++)
                {
                    if (i != 0) newPosition.x += xOffset;
                    positions[vectorsEntered++] = newPosition;
                }
                
                newPosition.x = negativeCameraAspect + xOffset * 0.5f;
            }

            _cardsPositions = positions;
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