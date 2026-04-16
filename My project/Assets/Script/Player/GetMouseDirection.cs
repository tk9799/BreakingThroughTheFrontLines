using UnityEngine;

/// <summary>
/// マウスの方向を取得するクラス
/// </summary>
public class GetMouseDirection : MonoBehaviour
{
    /// <summary>
    /// マウスの方向を取得するメソッド
    /// </summary>
    /// <returns></returns>
    public Vector2 MouseDirection() 
    {
        // マウスの座標を取得
        Vector3 mouseScreenPosition = Input.mousePosition;

        // マウスのスクリーン座標をワールド座標に変換
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // 2DゲームなのでZ座標を0に設定
        mouseWorldPosition.z = 0.0f;

        // プレイヤーの位置からマウスの位置への方向ベクトルを計算
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        // 方向ベクトルを返す
        return direction;
    }
}
