using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CardGame.UI;
using UnityEngine;

namespace CardGame
{
    public class MemoryGameManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _camera;
        [SerializeField] private BoardSo _boardData;
        [SerializeField] private Hud _hud;
        [SerializeField] private AudioClip _cardDragAudioClip;
        [SerializeField] private GameEvent _winGameEvent;

        [Space, Header("Cards")] [SerializeField]
        private Card _cardPrefab;

        [SerializeField] private CardSO[] _cardsSo;

        [Header("Animations")] [SerializeField]
        private Transform _instantiatePosition;

        [SerializeField] private Transform _deckPosition;
        [SerializeField] [Range(0, 0.2f)] private float _initialDelay;
        [SerializeField] [Range(0, 0.5f)] private float _moveCardToPositionDelay;

        private CardsSpawner _cardsSpawner;
        private List<Card> _cardsList = new List<Card>();
        private Vector3[] _cardsPositions;
        private Card _currentCard;
        private float _cardsScale;
        private int _matches;

        #endregion

        private void Start()
        {
            _cardsSpawner = new CardsSpawner(_boardData, _camera, _cardPrefab,
                _instantiatePosition, _deckPosition, _cardsSo, _initialDelay);

            _cardsPositions = _cardsSpawner.InitCardsPositions();
            _cardsList = _cardsSpawner.InitCards();
        }

        public async void StartGame()
        {
            AudioManager.Instance.PlaySound(_cardDragAudioClip, 1.5f);
            int timeToWait = 2;

            await Task.Delay(TimeSpan.FromSeconds(timeToWait));

            while (Time.timeScale < 0.9f)
                await Task.Delay(TimeSpan.FromSeconds(timeToWait - 1));

            MoveCardsToPosition();
        }

        public void RestartGame() //TODO: Restart game event
        {
            if (_currentCard) _currentCard = null;

            _hud.RestartGame();
            _cardsSpawner.RestartGame();
            _matches = 0;
            StartGame();
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

            _hud.StartCounter();
        }

        public void CheckMatch(Card card)
        {
            card.RotateCard(false);

            if (_currentCard == null) _currentCard = card;
            else if (_currentCard.CardNumber == card.CardNumber)
            {
                if ((_matches += 2) >= _cardsList.Count) _winGameEvent.Raise();
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
    }
}