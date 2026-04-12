using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    [SerializeField] private float targetDistance = 0.0f;

    private void Start()
    {
        Vector3 playerOffset=new Vector3(0, 0, -targetDistance);
    }
   
    private void Update()
    {
        transform.position = targetTransform.position;
        transform.position += new Vector3(0, 0, -targetDistance);
    }
}
