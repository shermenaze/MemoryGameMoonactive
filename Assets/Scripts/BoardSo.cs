using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(menuName = "Boards", fileName = "Board")]
    public class BoardSo : ScriptableObject
    {
        [SerializeField] private Transform _layout;
    }
}