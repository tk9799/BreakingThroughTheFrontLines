using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] public int damage = 0;

    // 弾のCollider2Dは IsTrigger = true にしておく
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵かどうか判定
        EnemyLife enemyLife = collision.GetComponent<EnemyLife>();
        if (enemyLife != null)
        {
            enemyLife.TakeDamage(damage);

            // 弾を消す（プール利用しているなら Release を呼ぶ）
            gameObject.SetActive(false);
            //BulletPool<BulletMove>.Release(this);
        }
    }
}
