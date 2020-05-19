using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(menuName = "Boards", fileName = "Board")]
    public class BoardSo : ScriptableObject
    {
        [SerializeField] private int _rows;
        [SerializeField] private int _columns;

        public int Rows => _rows;
        public int Columns => _columns;
    }
}