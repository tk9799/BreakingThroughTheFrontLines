using System;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int myHp = 0;

    private EnemyPool<EnemyMove> enemyPool;

    public static event Action<int> onAddScore;

    [Header("敵を倒したときに加算されるスコア")]
    [SerializeField] private int addScore = 0;

    /// <summary>
    /// Poolの参照をするためのメソッド
    /// </summary>
    public void SetPool(EnemyPool<EnemyMove> pool)
    {
        enemyPool = pool;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("ダメージを受けた");
        myHp -= damage;
        if (myHp <= 0)
        {
            DieProcess();
        }
    }

    private void DieProcess()
    {
        onAddScore?.Invoke(addScore);

        enemyPool.Release(GetComponent<EnemyMove>());
    }
}
