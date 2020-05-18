using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CardGame
{
    public class CardsGameManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _camera;
        [SerializeField] private BoardSo _boardData;

        [Header("Cards")]
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private CardSO[] _cardsSo;

        [Header("Animations")] [SerializeField]
        private Transform _instantiatePosition;

        [SerializeField] private Transform _deckPosition;
        [SerializeField] [Range(0, 0.2f)] private float _initialDelay;
        [SerializeField] [Range(0, 0.5f)] private float _moveCardToPositionDelay;

        [Header("GameEvents")] [SerializeField]
        private GameEvent _gameStartEvent;

        private List<Card> _cardsList = new List<Card>();
        private Vector3[] _cardsPositions;
        private Card _currentCard;
        
        private float _cardsScale;
        private int _matches;

        #endregion

        private void Start()
        {
            CardsSpawner cardsSpawner = new CardsSpawner(_boardData, _camera, _cardPrefab,
                _instantiatePosition, _deckPosition, _cardsSo, _initialDelay);

            _cardsPositions = cardsSpawner.InitCardsPositions();
            _cardsList = cardsSpawner.InitCards();
            StartGame();
        }
        
        private async void StartGame()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
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
    }
}
