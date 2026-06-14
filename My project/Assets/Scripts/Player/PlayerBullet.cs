using UnityEngine;

/// <summary>
/// プレイヤー弾クラス
/// </summary>
public class PlayerBullet : MonoBehaviour
{
    [Header("弾速")]
    [SerializeField] private float speed = 10f;

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        // 上方向へ移動
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}