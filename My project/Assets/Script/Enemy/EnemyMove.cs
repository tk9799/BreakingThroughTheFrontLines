using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform targetTransform;

    [SerializeField] private float moveSpeed = 0.0f;

    
    private void Update()
    {
        transform.LookAt(targetTransform);

        transform.position += transform.forward * moveSpeed;
    }
}
