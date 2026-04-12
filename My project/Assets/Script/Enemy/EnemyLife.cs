using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int myHp = 0;

    private EnemyPool<EnemyMove> enemyPool;

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
        enemyPool.Release(GetComponent<EnemyMove>());
    }
}
