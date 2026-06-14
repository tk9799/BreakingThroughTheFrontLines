using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの移動クラス
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("移動スピード")]
    [SerializeField] private float moveSpeed = 5.0f;

    //Rigidbody2Dコンポーネント
    private Rigidbody2D rb;

    //入力された移動方向
    private Vector2 moveInput;

    /// <summary>
    /// 移動スピード
    /// </summary>
    public float MoveSpeed => moveSpeed;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //コンポーネント取得
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// プレイヤーの移動入力を取得
    /// </summary>
    public void OnMove(InputValue value)
    {
        //入力された移動方向を取得
        moveInput = value.Get<Vector2>();
    }

    /// <summary>
    /// 物理演算更新処理
    /// </summary>
    private void FixedUpdate()
    {
        //プレイヤーを移動
        rb.linearVelocity = moveInput * moveSpeed;
    }
}