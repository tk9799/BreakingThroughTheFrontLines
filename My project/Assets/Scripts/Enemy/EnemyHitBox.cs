using UnityEngine;

/// <summary>
/// 敵の当たり判定クラス
/// </summary>
public class EnemyHitBox : MonoBehaviour
{
    [Header("敵ステータス")]
    [SerializeField] private EnemyStatus enemyStatus = null;

    /// <summary>
    /// 当たり判定処理
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //弾を取得
        BulletBase bullet = collision.GetComponent<BulletBase>();

        //敵ステータスが設定されていない
        if (enemyStatus == null)
        {
            return;
        }

        //弾以外は無視
        if (bullet == null)
        {
            return;
        }

        //プレイヤー弾以外は無視
        if (bullet.Owner != BulletOwner.Player)
        {
            return;
        }

        //ダメージ
        enemyStatus.Damage(1);

        //PoolObject取得
        PoolObject poolObject = collision.GetComponent<PoolObject>();

        //弾を削除
        if (poolObject != null)
        {
            poolObject.Release();
        }
        else
        {
            //Destroy(bullet.gameObject);
        }
    }
}