using UnityEngine;

[CreateAssetMenu(menuName = "Cards", fileName = "Card")]
public class CardSO : ScriptableObject
{
    [SerializeField] private Sprite _cardSprite;
    
    public Sprite CardSprite => _cardSprite;
}
