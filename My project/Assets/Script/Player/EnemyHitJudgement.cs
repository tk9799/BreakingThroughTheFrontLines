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
        myLife -= decreaseLife;
        Debug.Log("Player Life : " + myLife);
    }

    // 無敵時間開始
    private void StartInvincible()
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibleCoroutine());
        }
    }

    private System.Collections.IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;

        // 当たり判定無効
        GetComponent<Collider2D>().enabled = false;
        Debug.Log("Player is Invincible!");

        yield return new WaitForSeconds(invincibleTime);

        // 当たり判定無効解除
        isInvincible = false;

        GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Player Hit by Enemy!");
            if (!isInvincible)
            {
                
                // 登録されている全ての処理を実行
                onEnemyHitProcess?.Invoke();
            }
        }
    }
}
