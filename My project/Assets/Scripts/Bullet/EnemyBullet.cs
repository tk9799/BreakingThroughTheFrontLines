using UnityEngine;

/// <summary>
/// 敵弾クラス
/// </summary>
public class EnemyBullet : BulletBase
{
    /// <summary>
    /// カメラ外に出た時の処理
    /// </summary>
    private void OnBecameInvisible()
    {
        //プールへ返却
        Despawn();
    }
}