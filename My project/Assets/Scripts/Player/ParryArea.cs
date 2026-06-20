using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パリィ判定クラス
/// </summary>
public class ParryArea : MonoBehaviour
{
    [Header("プレイヤーステータス")]
    [SerializeField] private PlayerStatus playerStatus = null;

    //範囲内の敵弾リスト
    private readonly List<BulletBase> bullets = new List<BulletBase>();

    /// <summary>
    /// 範囲内に敵弾があるか
    /// </summary>
    public bool HasEnemyBullet => bullets.Count > 0;

    /// <summary>
    /// 範囲に入っている
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //衝突したオブジェクトから弾コンポーネントを取得
        BulletBase bullet = collision.GetComponent<BulletBase>();

        // 弾以外は無視
        if (bullet == null)
        {
            return;
        }

        //敵弾またはボス弾のみ対象
        if (bullet.Owner == BulletOwner.Enemy ||
            bullet.Owner == BulletOwner.Boss)
        {
            playerStatus.SetParry(true);
        }

        //
        if (!bullets.Contains(bullet))
        {
            bullets.Add(bullet);
        }

        //
        playerStatus.SetParry(true);
    }

    /// <summary>
    /// 範囲から外れる
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        //衝突したオブジェクトから弾コンポーネントを取得
        BulletBase bullet = collision.GetComponent<BulletBase>();

        //弾以外は無視
        if (bullet == null)
        {
            return;
        }

        if (bullet.Owner != BulletOwner.Enemy &&
        bullet.Owner != BulletOwner.Boss)
        {
            return;
        }

        //リストから削除
        bullets.Remove(bullet);

        //
        if (bullets.Count == 0)
        {
            playerStatus.SetParry(false);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void UpdateParryState()
    {
        playerStatus.SetParry(bullets.Count > 0);
    }
}