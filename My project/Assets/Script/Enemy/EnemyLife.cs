using System;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [Header("敵のHP")]
    [SerializeField] private int myHp = 0;

    // 敵用のPool
    private EnemyPool<EnemyMove> enemyPool;

    // スコア加算のAction変数
    public static event Action<int> onAddScore;

    [Header("敵を倒したときに加算されるスコア")]
    [SerializeField] private int addScore = 0;

    private SpawnEnemy spawnEnemy;

    // 敵が倒れたときのAction変数
    public static event Action<GameObject> OnEnemyDead;

    /// <summary>
    /// Poolの参照をするためのメソッド
    /// </summary>
    public void SetPool(EnemyPool<EnemyMove> pool)
    {
        enemyPool = pool;
    }

    public void SetSpawner(SpawnEnemy spawner)
    {
        spawnEnemy = spawner;
    }

    /// <summary>
    /// ダメージ計算をするメソッド
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        Debug.Log("ダメージを受けた");

        // 敵のHPからダメージ値を引く
        myHp -= damage;

        // HPが0以下になったら敵を倒す処理をする
        if (myHp <= 0)
        {
            DieProcess();
        }
    }

    /// <summary>
    /// 敵が倒れた時のメソッド
    /// </summary>
    private void DieProcess()
    {
        // スコア加算のActionを呼び出し他のクラスにあるonAddScoreも実行する
        onAddScore?.Invoke(addScore);

        // 敵が倒れたときのActionを呼び出し他のクラスにあるOnEnemyDeadも実行する
        OnEnemyDead?.Invoke(gameObject);
        Debug.Log("敵を倒した");
        // 敵をPoolに返す
        enemyPool.Release(GetComponent<EnemyMove>());
    }
}
