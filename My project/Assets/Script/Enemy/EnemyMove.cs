using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // 追いかけるオブジェクトの座標
    // 他のクラスから対象の座標を参照するので変数だけ作る
    public Transform targetTransform;

    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 0.0f;

    
    private void Update()
    {
        // 追いかけるオブジェクトの座標に向かって移動する
        transform.position =Vector2.MoveTowards(transform.position, 
            targetTransform.position, moveSpeed * Time.deltaTime);
    }
}
