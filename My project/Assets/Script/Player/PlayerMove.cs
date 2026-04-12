using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.0f;

    [SerializeField] private float rotateSpeed = 0.0f;

    [SerializeField] private Transform cameraTransform;

    private void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        transform.position += (Vector3)(input.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
