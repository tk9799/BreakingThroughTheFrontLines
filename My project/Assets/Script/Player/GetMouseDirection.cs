using UnityEngine;

/// <summary>
/// マウスの方向を取得するクラス
/// </summary>
public class GetMouseDirection : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// マウスの方向を取得するメソッド
    /// </summary>
    /// <returns></returns>
    public Vector2 MouseDirection() 
    {
        if (Camera.main == null)
        {
            return Vector2.right;
        }

        // マウスの座標を取得
        Vector3 mouseScreenPosition = Input.mousePosition;

        // マウスのスクリーン座標をワールド座標に変換
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        // 2DゲームなのでZ座標を0に設定
        mouseWorldPosition.z = 0.0f;

        // プレイヤーの位置からマウスの位置への方向ベクトルを計算
        //Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        // 方向ベクトルを返す
        return (mouseWorldPosition - transform.position).normalized;
    }
}
