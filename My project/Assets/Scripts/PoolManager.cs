using UnityEngine;

/// <summary>
/// プール管理クラス
/// </summary>
public class PoolManager : SingletonMonobehaviour<PoolManager>
{
    [Header("プレイヤー弾")]
    [SerializeField] private ObjectPool playerBulletPool = null;

    [Header("敵弾")]
    [SerializeField] private ObjectPool[] enemyBulletPool = null;

    /// <summary>
    /// プレイヤー弾プール
    /// </summary>
    public ObjectPool PlayerBulletPool => playerBulletPool;

    /// <summary>
    /// 敵弾プール
    /// </summary>
    public ObjectPool[] EnemyBulletPool => enemyBulletPool;
}