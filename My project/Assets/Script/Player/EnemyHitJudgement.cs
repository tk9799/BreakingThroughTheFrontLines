using System;
using UnityEngine;

public class EnemyHitJudgement : MonoBehaviour
{
    [Header("プレイヤーのライフ")]
    [SerializeField] public int myLife = 3;

    [Header("被弾後の無敵時間")]
    [SerializeField] private float invincibleTime = 1.0f;

    // ライフを減らす量
    private int decreaseLife = 1;

    // 無敵状態中かどうか
    private bool isInvincible = false;

    // 敵に当たったら発動するイベント（Action）
    public Action onEnemyHitProcess;

    private void Start()
    {
        // Action に減少処理を登録
        onEnemyHitProcess += PlayerDecreaseLife;
        onEnemyHitProcess += StartInvincible;
    }

    // ライフを減らす処理
    private void PlayerDecreaseLife()
    {
        // ライフを減らす
        myLife -= decreaseLife;
        Debug.Log("Player Life : " + myLife);
    }

    /// <summary>
    /// 無敵時間を開始するメソッド
    /// </summary>
    private void StartInvincible()
    {
        // 無敵状態でない場合のみ、無敵状態にする
        if (!isInvincible)
        {
            StartCoroutine(InvincibleCoroutine());
        }
    }

    /// <summary>
    /// プレイヤーの当たり判定を一定時間無効にして
    /// ライフを減らさないようにするコルーチン
    /// </summary>
    private System.Collections.IEnumerator InvincibleCoroutine()
    {
        // 当たり判定が無効になっているときにtureにする
        isInvincible = true;

        // 当たり判定無効
        GetComponent<Collider2D>().enabled = false;
        Debug.Log("Player is Invincible!");

        // invincibleTimeで設定した時間の後に下の処理を行う
        yield return new WaitForSeconds(invincibleTime);

        // 当たり判定無効解除
        isInvincible = false;

        // 当たり判定有効
        GetComponent<Collider2D>().enabled = true;
    }

    /// <summary>
    /// プレイヤーの当たり判定を行うメソッド
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 当たったオブジェクトのTagがEnemyだった場合
        if (collision.CompareTag("Enemy"))
        {
            // 当たり判定が有効の場合のみ、被弾処理を行う
            Debug.Log("Player Hit by Enemy!");
            if (!isInvincible)
            {
                // 登録されている全ての処理を実行
                onEnemyHitProcess?.Invoke();
            }
        }
    }
}
