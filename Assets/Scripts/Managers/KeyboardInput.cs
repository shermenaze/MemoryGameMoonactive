using UnityEngine;

namespace CardGame
{
    public class KeyboardInput : IInput
    {
        private readonly GameEventBool _gamePauseEvent;

        public KeyboardInput(GameEventBool gamePauseEvent)
        {
            _gamePauseEvent = gamePauseEvent;
        }
        
        public void CheckInput(Camera camera)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) _gamePauseEvent.Raise();

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

                if (hit) hit.transform.GetComponent<IClickable>()?.Click();
            }
        }
    }
}