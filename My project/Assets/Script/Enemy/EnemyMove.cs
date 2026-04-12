using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Transform targetTransform;

    [SerializeField] private float moveSpeed = 0.0f;

    
    private void Update()
    {
        transform.position =Vector2.MoveTowards(transform.position, 
            targetTransform.position, moveSpeed * Time.deltaTime);
        //Debug.Log(transform.position);
    }
}
