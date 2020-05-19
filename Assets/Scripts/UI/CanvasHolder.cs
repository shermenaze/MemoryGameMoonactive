using UnityEngine;

namespace CardGame.UI
{
    public class CanvasHolder : MonoBehaviour
    {
        [SerializeField] private Canvas _canvasBackground;
        
        public void Init(Camera cam) => _canvasBackground.worldCamera = cam;
    }
}