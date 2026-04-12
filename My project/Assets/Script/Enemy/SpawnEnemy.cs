using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class SpawnEnemyPosition 
{ 
    [Header("アイテムを配置する部屋の座標範囲を設定する")]
    [Header("部屋の最小X座標")] 
    public float minX; 

    [Header("部屋の最大X座標")] 
    public float maxX; 

    [Header("部屋の最小Z座標")] 
    public float minY; 

    [Header("部屋の最大Z座標")] 
    public float maxY; 
}

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] public SpawnEnemyPosition[] spawnEnemyPositions;
    [SerializeField] private GameObject enemyObject;

    private float time = 0.0f;
    [SerializeField] private float spawnTime = 1.0f;

    private EnemyPool<EnemyMove> enemyPool = null;

    public List<GameObject> enemyList = new List<GameObject>();

    [Header("配置する敵の最大値")]
    [SerializeField] public int maxEnemyObjectCount = 10;

    [Header("敵が追従するターゲット")]
    [SerializeField] private Transform targetTransform;

    private void Start()
    {
        enemyPool = new EnemyPool<EnemyMove>(enemyObject.GetComponent<EnemyMove>(), 15, transform);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > spawnTime && enemyList.Count < maxEnemyObjectCount)
        {
            EnemySpawn();
            time = 0.0f;
        }

        
    }

    // ランダム座標を返す
    private Vector3 GetRandomPosition()
    {
        // 配列からランダムに 1つ選ぶ
        int index = Random.Range(0, spawnEnemyPositions.Length);
        SpawnEnemyPosition spawnEnemyPosition = spawnEnemyPositions[index];

        float randomX = Random.Range(spawnEnemyPosition.minX, spawnEnemyPosition.maxX);
        float randomY = Random.Range(spawnEnemyPosition.minY, spawnEnemyPosition.maxY);

        return new Vector3(randomX, randomY, 0.0f);
    }

    private void EnemySpawn()
    {
        // プールから取得
        EnemyMove enemyMove = enemyPool.Get();

        // ランダム位置に配置
        enemyMove.transform.position = GetRandomPosition();

        enemyMove.targetTransform = targetTransform;

        // 敵のHPを管理するクラスにプールを渡す
        enemyMove.GetComponent<EnemyLife>().SetPool(enemyPool);

        // 管理リストに追加
        enemyList.Add(enemyMove.gameObject);
    }
}
