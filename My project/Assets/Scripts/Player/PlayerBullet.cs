using UnityEngine;

/// <summary>
/// プレイヤー弾クラス
/// </summary>
public class PlayerBullet : BulletBase
{
    /// <summary>
    /// カメラ外に出た時の処理
    /// </summary>
    private void OnBecameInvisible()
    {
        //弾を削除
        Destroy(gameObject);
    }
}