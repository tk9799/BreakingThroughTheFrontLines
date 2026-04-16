using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// プレイヤーの移動だけを制御するクラス
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [Header("プレイヤーの移動速度")]
    [SerializeField] private float moveSpeed = 0.0f;

    [Header("プレイヤーの回転速度")]
    [SerializeField] private float rotateSpeed = 0.0f;

    private void FixedUpdate()
    {
        // キーボードの入力を取得
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // 入力した方向にプレイヤーを移動させる
        transform.position += (Vector3)(input.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
