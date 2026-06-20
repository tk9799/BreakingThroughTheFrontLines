using UnityEngine;

/// <summary>
/// プレイヤーの当たり判定
/// </summary>
public class PlayerHitBox : MonoBehaviour
{
    [Header("プレイヤーステータス")]
    [SerializeField] private PlayerStatus playerStatus = null;

    /// <summary>
    /// 当たり判定処理
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //弾を取得
        BulletBase bullet = collision.GetComponent<BulletBase>();

        //弾じゃない
        if ((bullet == null))
        {
            return;
        }

        //敵＆ボス弾以外は無視
        if (bullet.Owner != BulletOwner.Enemy && bullet.Owner != BulletOwner.Boss)
        {
            return;
        }

        //
        if (playerStatus != null && playerStatus.IsParryActive)
        {
            return;
        }

        //ダメージ
        playerStatus.Damage();

        //弾を削除
        Destroy(bullet.gameObject);
    }
}