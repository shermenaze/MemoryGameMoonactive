using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(menuName = "Messages/HeaderMessage", fileName = "HeaderMessage")]
    public class HeaderMessageSO : ScriptableObject
    {
        [SerializeField] private string _message;

        public string Message => _message;
    }
}