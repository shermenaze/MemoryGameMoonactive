using UnityEngine;

namespace CardGame.UI
{
    public class UiScreen :MonoBehaviour
    {
        [SerializeField] private HeaderMessageSO _message;

        public HeaderMessageSO MessageSo => _message;
    }
}