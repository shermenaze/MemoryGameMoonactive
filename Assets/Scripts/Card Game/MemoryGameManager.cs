using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CardGame.UI;
using UnityEngine;
using Random = UnityEngine.Random;

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

        [Space, Header("Cards")]
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private CardSO[] _cardsSo;

        [Header("Animations")]
        [SerializeField] private Transform _initialTransform;
        [SerializeField] private Transform _deckTransform;
        
        [Tooltip("Time to add to each animation of Card moving towards Deck")]
        [SerializeField] [Range(0, 0.2f)] private float _initialDelay = 0.05f;
        [Tooltip("Time to add to each animation of Card starts moving towards end position")]
        [SerializeField] [Range(0, 0.5f)] private float _moveCardToPositionDelay = 0.1f;
        [Tooltip("How long can the player watch the flipped cards for the first time?")]
        [SerializeField] [Range(0.5f, 10f)]private float _timeBeforeCardsHide = 5f;

        private CardsSpawner _cardsSpawner;
        private List<Card> _cardsList = new List<Card>();
        private Vector3[] _cardsPositions;
        private Card _currentCard;
        private float _cardsScale;
        private int _matches;

        #endregion

        #region Properties

        public BoardSo BoardData => _boardData;
        public Camera CurrentCamera => _camera;
        public Card CardPrefab => _cardPrefab;
        public Transform InitialTransform => _initialTransform;
        public Transform DeckTransform => _deckTransform;
        public CardSO[] CardsSo => _cardsSo;
        public float InitialDelay => _initialDelay;
        
        #endregion

        public void Init(Camera cam, Hud hud)
        {
            _camera = cam;
            _hud = hud;
        }
        
        private void Start()
        {
            _cardsSpawner = new CardsSpawner(this);
            
            _cardsPositions = _cardsSpawner.InitCardsPositions();
            _cardsList = _cardsSpawner.InitCards();
        }

        private void ShuffleCardsPositions(Vector3[] positions)
        {
            //Fisher-Yates shuffle
            
            int currentIndex = positions.Length;

            while (currentIndex != 0)
            {
                var randomIndex = Mathf.FloorToInt(Random.value * currentIndex);
                currentIndex -= 1;
                
                var tempValue = positions[currentIndex];
                positions[currentIndex] = positions[randomIndex];
                positions[randomIndex] = tempValue;
            }
        }
        
        public async void StartGame()
        {
            AudioManager.Instance.PlaySound(_cardDragAudioClip, 1.5f);
            int timeToWait = 2;

            await Task.Delay(TimeSpan.FromSeconds(timeToWait));

            MoveCardsToPosition();
        }

        public void RestartGame()
        {
            if (_currentCard) _currentCard = null;
            
            ShuffleCardsPositions(_cardsPositions);
            _cardsSpawner.RestartGame();
            _matches = 0;
            StartGame();
        }

        /// <summary>
        /// Moves all cards toward their designated end position
        /// </summary>
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

        /// <summary>
        /// Show all cards, wait for _timeBeforeCardsHide seconds, and Hide them all; Starts game counter
        /// </summary>
        private IEnumerator ShowAllCards()
        {
            float duration = 0;

            _cardsList.ForEach(card => card.RotateCard(false, duration += 0.1f));
            yield return new WaitForSeconds(_timeBeforeCardsHide);
            _cardsList.ForEach(card => card.RotateCard(true));

            _hud.StartCounter();
        }

        /// <summary>
        /// Check if Card is a match to previously selected Card
        /// </summary>
        /// <param name="card">The current chosen Card</param>
        public void CheckMatch(Card card)
        {
            card.RotateCard(false);

            if (_currentCard == null) _currentCard = card;
            else if (_currentCard.CardNumber == card.CardNumber)
            {
                if ((_matches += 2) >= _cardsList.Count) _winGameEvent.Raise();
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