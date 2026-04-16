using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のスポーンする範囲を設定するクラス
/// </summary>
[System.Serializable] 
public class SpawnEnemyPosition 
{ 
    [Header("敵がスポーンする座標範囲を設定する")]
    [Header("敵の最小X座標")] 
    public float minX; 

    [Header("敵の最大X座標")] 
    public float maxX; 

    [Header("敵の最小Y座標")] 
    public float minY; 

    [Header("敵の最大Y座標")] 
    public float maxY; 
}

/// <summary>
/// 敵がランダムの位置にスポーンするクラス
/// </summary>
public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] public SpawnEnemyPosition[] spawnEnemyPositions;

    [Header("スポーンさせる敵のPrefab")]
    [SerializeField] private GameObject enemyObject;

    // 敵がスポーンするタイマー
    private float time = 0.0f;

    [Header("敵がスポーンする時間間隔")]
    [SerializeField] private float spawnTime = 0.0f;

    // 敵用のPool
    private EnemyPool<EnemyMove> enemyPool = null;

    // スポーンした敵のList
    public List<GameObject> enemyList = new List<GameObject>();

    [Header("配置する敵の最大値")]
    [SerializeField] public int maxEnemyObjectCount = 0;

    [Header("敵が追従するターゲット")]
    [SerializeField] private Transform targetTransform;

    private void Start()
    {
        enemyPool = new EnemyPool<EnemyMove>(enemyObject.GetComponent<EnemyMove>(), maxEnemyObjectCount, transform);
    }

    private void Update()
    {
        // タイマーを進める
        time += Time.deltaTime;

        // タイマーがスポーンする時間を超えて敵がスポーンする最大値を
        // 超えていないときにスポーンする
        if (time > spawnTime && enemyList.Count < maxEnemyObjectCount)
        {
            EnemySpawn();
            time = 0.0f;
        }
    }

    /// <summary>
    /// 敵をスポーンする座標をランダムに決めるメソッド
    /// </summary>
    private Vector3 GetRandomPosition()
    {
        // 配列からランダムに 1つ選ぶ
        int index = Random.Range(0, spawnEnemyPositions.Length);

        // 選んだ範囲を取得する
        SpawnEnemyPosition spawnEnemyPosition = spawnEnemyPositions[index];

        // 選んだ範囲からランダムな座標を生成する
        float randomX = Random.Range(spawnEnemyPosition.minX, spawnEnemyPosition.maxX);
        float randomY = Random.Range(spawnEnemyPosition.minY, spawnEnemyPosition.maxY);

        return new Vector3(randomX, randomY, 0.0f);
    }

    /// <summary>
    /// スポーンさせると同時に必要な情報を敵に渡すメソッド
    /// </summary>
    private void EnemySpawn()
    {
        // プールから取得
        EnemyMove enemyMove = enemyPool.Get();

        // ランダム位置に配置
        enemyMove.transform.position = GetRandomPosition();

        // 敵が追いかけるターゲットを代入する
        enemyMove.targetTransform = targetTransform;

        // 敵のHPを管理するクラスにプールを渡す
        enemyMove.GetComponent<EnemyLife>().SetPool(enemyPool);

        // 管理リストに追加
        enemyList.Add(enemyMove.gameObject);
    }

    /// <summary>
    /// オブジェクト有効になった時に自動で呼ばれる関数
    /// 敵が倒れたときのイベント登録
    /// </summary>
    private void OnEnable()
    {
        EnemyLife.OnEnemyDead += RemoveEnemy;
    }

    /// <summary>
    /// オブジェクト無効になった時に自動で呼ばれる関数
    /// </summary>
    private void OnDisable()
    {
        EnemyLife.OnEnemyDead -= RemoveEnemy;
    }

    /// <summary>
    /// euenemyListから敵を削除する関数
    /// </summary>
    /// <param name="enemy"></param>
    private void RemoveEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }
}
