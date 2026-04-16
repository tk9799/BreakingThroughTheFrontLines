using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [Header("敵に与えるダメージ量")]
    [SerializeField] public int damage = 0;

    // 弾の移動処理をするクラスの変数
    [SerializeField] private BulletMove bulletMove;

    /// <summary>
    /// 弾の当たり判定をして、敵だった場合ダメージ処理をする処理
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵かどうか判定
        EnemyLife enemyLife = collision.GetComponent<EnemyLife>();
        if (enemyLife != null)
        {
            // ダメージ計算メソッドを呼び出し、弾で設定したダメージ値を引数にする
            enemyLife.TakeDamage(damage);

            // 弾をPoolに返却するメソッドを呼び出す
            bulletMove.BulletRelease();
        }
    }
}
