using UnityEngine;

namespace CardGame
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameEventBool _gamePauseEvent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) _gamePauseEvent.Raise();

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

                if (hit) hit.transform.GetComponent<IClickable>()?.Clicked();
            }
        }
    }
}