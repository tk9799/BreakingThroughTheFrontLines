using UnityEngine;

/// <summary>
///  オブジェクトプールを管理するクラス
/// </summary>
public class PoolManager : SingletonMonobehaviour<PoolManager>
{
    [Header("プレイヤー弾")]
    [SerializeField] private ObjectPool playerBulletPool = null;

    [Header("敵弾")]
    [SerializeField] private ObjectPool[] enemyBulletPool = null;

    [Header("ボス弾")]
    [SerializeField] private ObjectPool[] bossBulletPool = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected override void Awake()
    {
        //シングルトンの初期化
        base.Awake();

        //プレイヤー弾プールの設定確認
        if (playerBulletPool == null)
        {
            Debug.LogError("PlayerBulletPoolが設定されていません。");
        }

        //敵弾プールの設定確認
        if (enemyBulletPool == null || enemyBulletPool.Length == 0)
        {
            Debug.LogError("EnemyBulletPoolが設定されていません。");
            return;
        }

        ////ボス弾プールの設定確認
        //if (bossBulletPool == null || bossBulletPool.Length == 0)
        //{
        //    Debug.LogError("BossBulletPoolが設定されていません。");
        //    return;
        //}

        //敵弾プール内の設定漏れを確認
        for (int i = 0; i < enemyBulletPool.Length; i++)
        {
            if (enemyBulletPool[i] == null)
            {
                Debug.LogError($"EnemyBulletPool[{i}]が未設定です。");
            }
        }

        ////ボス弾プール内の設定漏れを確認
        //for (int i = 0; i < bossBulletPool.Length; i++)
        //{
        //    if (bossBulletPool[i] == null)
        //    {
        //        Debug.LogError($"BossBulletPool[{i}]が未設定です。");
        //    }
        //}
    }

    /// <summary>
    /// プレイヤー弾を取得
    /// </summary>
    public GameObject GetPlayerBullet()
    {
        return playerBulletPool.Get();
    }

    /// <summary>
    /// 敵弾を取得
    /// </summary>
    public GameObject GetEnemyBullet(int index)
    {
        //配列の範囲外アクセスを防ぐ
        if (index < 0 || index >= enemyBulletPool.Length)
        {
            Debug.LogError($"EnemyBulletPool[{index}]は存在しません。");
            return null;
        }

        return enemyBulletPool[index].Get();
    }

    /// <summary>
    /// ボス弾を取得
    /// </summary>
    public GameObject GetBossBullet(int index)
    {
        //配列の範囲外アクセスを防ぐ
        if (index < 0 || index >= bossBulletPool.Length)
        {
            Debug.LogError($"BossBulletPool[{index}]は存在しません。");
            return null;
        }

        return bossBulletPool[index].Get();
    }
}