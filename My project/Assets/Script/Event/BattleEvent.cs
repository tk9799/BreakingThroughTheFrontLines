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

    //[SerializeField] public EnemySpawnPattern[] spawnPatterns;

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
        // 敵を出現させる情報が書かれているデータをランダムで選ぶ
        SpawnPositionsData spawnData = spawnPositionsData[Random.Range(0, spawnPositionsData.Length)];
        Debug.Log(spawnData);
        //  enemyCountとspawnPositionの少ない方を使用
        int spawnCount = Mathf.Min(spawnData.enemyCount, spawnData.spawnPosition.Length);

        for(int i = 0; i < spawnCount; i++)
        {
            GameObject enemyObject = GetRandomEnemyObject();

            Instantiate(enemyObject,
                transform.position + (Vector3)spawnData.spawnPosition[i],
                Quaternion.identity);
        }
    }
}
