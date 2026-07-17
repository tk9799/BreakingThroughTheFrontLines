using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵を出現させる座標
/// </summary>
[System.Serializable]
public class EnemySpawnPattern
{
    public Transform[] spawnPosition;
}

[System.Serializable]
public class EnemyKinds
{
    public GameObject enemyObject;
}

public class BattleEvent : MonoBehaviour
{

    [SerializeField] public EnemyKinds[] enemyKinds;

    [SerializeField] public EnemySpawnPattern[] spawnPatterns;

    [SerializeField] private SpawnPositionsData[] spawnPositionsData;

    [SerializeField] private int appearNumber = 0;

    /// <summary>
    /// 出現させる敵をランダムに選ぶ
    /// </summary>
    /// <returns></returns>
    public GameObject GetRandomEnemyObject()
    {
        int appearEnemyNumber= Random.Range(0, enemyKinds.Length);

        return enemyKinds[appearEnemyNumber].enemyObject;
    }

    public void AppearEnemy()
    {
        EnemySpawnPattern pattern = spawnPatterns[Random.Range(0, spawnPatterns.Length)];

        SpawnPositionsData spawnData = spawnPositionsData[Random.Range(0, spawnPatterns.Length)];

        List<Transform> spawnPoints = new List<Transform>(pattern.spawnPosition);

        // 配列の一番小さい番号を探す
        //int spawnCount = Mathf.Min(appearNumber, spawnPoints.Count);

        foreach(Vector2 spawnDataPosition in spawnData.spawnPosition)
        {
            GameObject enemyObject = enemyKinds[Random.Range(0,enemyKinds.Length)].enemyObject;

            Instantiate(enemyObject, transform.position + (Vector3)spawnDataPosition, Quaternion.identity);
            Debug.Log(enemyObject.transform.position);
        }

        // 出現させる敵の数だけ敵を配置
        //for (int i = 0; i < spawnCount; i++)
        //{
        //    int index = Random.Range(0, spawnPoints.Count);

        //    // index番目の配列の座標を取得
        //    Transform spawnPoint = spawnPoints[index];

        //    Instantiate(GetRandomEnemyObject(),spawnPoint.position , Quaternion.identity);

        //    // 出現位置が被らないように削除
        //    spawnPoints.RemoveAt(index);
        //}
    }
}
