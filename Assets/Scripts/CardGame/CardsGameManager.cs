using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class CardsGameManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameEvent _gameStartEvent;
        [SerializeField] private Transform _instantiatePosition;
        [SerializeField] private Transform _deckPosition;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private CardSO[] _cardsSo;
        [SerializeField] private Camera _camera;
        [SerializeField] [Range(0, 0.2f)] private float _initialDelay;
        [SerializeField] [Range(0, 0.5f)] private float _moveCardToPositionDelay;

        [SerializeField] private BoardSo _boardData; //TODO: Add functionality or delete

        private readonly List<Card> _cardsList = new List<Card>();
        private Vector3[] _cardsPositions;
        private float _cardsScale;
        private Card _currentCard;
        private int _matches;

        private const int Rows = 4;
        private const int Columns = 4;
        private const int CardsAmount = Rows * Columns;

        #endregion

        private void Start()
        {
            InitCardsPositions();
            InitCards();
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
            newPosition.y = negativeCameraAspect + yOffset * 0.5f - _camera.aspect * 3f; //TODO: Not like this

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

        private void InitCards()
        {
            var usedNumbers = new List<int>();
            float animationDelay = 0;
            int orderInLayer = 0;

            for (int cardIndex = 0; cardIndex < CardsAmount; cardIndex++)
            {
                var card = Instantiate(_cardPrefab, _instantiatePosition);
                card.transform.localScale *= _cardsScale;
                var randomNumber = GetRandomNumber(_cardsSo, usedNumbers);
                var cardSo = _cardsSo[randomNumber];

                card.Init(this, cardSo, randomNumber, orderInLayer += 2);
                _cardsList.Add(card);
                card.Animate(_deckPosition.position, animationDelay);
                animationDelay += _initialDelay;
            }

            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(3);

            MoveCardsToPosition();
        }

        private void MoveCardsToPosition()
        {
            float animationDelay = 0;

            for (int cardIndex = 0; cardIndex < _cardsList.Count; cardIndex++)
            {
                animationDelay += _moveCardToPositionDelay;
                _cardsList[cardIndex].Animate(_cardsPositions[cardIndex], animationDelay);
            }

            StartCoroutine(ShowAllCards());
        }

        private IEnumerator ShowAllCards()
        {
            float duration = 0;
            
            _cardsList.ForEach(card => card.RotateCard(false, duration += 0.1f));
            yield return new WaitForSeconds(5f);
            _cardsList.ForEach(card => card.RotateCard(true));
            
            _gameStartEvent.Raise();
        }

        public void CheckMatch(Card card)
        {
            card.RotateCard(false);

            if (_currentCard == null) _currentCard = card;
            else if (_currentCard.CardNumber == card.CardNumber)
            {
                if ((_matches += 2) >= _cardsList.Count) MemoryGameWon();
                //TODO: Activate both cards particle systems.
                _currentCard = null;
            }
            else
            {
                card.RotateCard(true, 0.4f);
                _currentCard.RotateCard(true, 0.4f);
                _currentCard = null;
            }
        }

        private void MemoryGameWon()
        {
            Debug.Log("Win!");
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