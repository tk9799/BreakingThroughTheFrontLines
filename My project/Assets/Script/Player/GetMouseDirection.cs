using UnityEngine;

public class GetMouseDirection : MonoBehaviour
{
    public Vector2 MouseDirection() 
    { 
        Vector3 mouseScreenPosition = Input.mousePosition; 

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        mouseWorldPosition.z = 0.0f; 

        Vector2 direction = (mouseWorldPosition - transform.position).normalized;
        //Debug.Log("Mouse Direction: " + direction);
        return direction;
    }
}
