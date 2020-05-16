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
        [SerializeField] private BoardSo _boardData;//TODO: Add functionality or delete

        private const int CardsAmount = 16;
        private readonly List<Card> _cardsList = new List<Card>();

        private void Start()
        {
            var usedNumbers = new List<int>();

            for (int cardIndex = 0; cardIndex < CardsAmount; cardIndex++)
            {
                var card = Instantiate(_cardPrefab, _instantiatePosition);
                var randomNumber = GetRandomNumber(_cardsSo, usedNumbers);
                var cardSo = _cardsSo[randomNumber];
                
                card.Init(this, cardSo, randomNumber);
                _cardsList.Add(card);   
            }
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