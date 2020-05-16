using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);
        
        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

        if (hit) hit.transform.GetComponent<IClickable>()?.Clicked();
    }
}
