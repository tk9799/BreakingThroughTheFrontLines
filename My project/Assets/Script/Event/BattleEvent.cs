using UnityEngine;

[System.Serializable]
public class RandomAppearPosition
{
    public float minX = 0.0f;

    public float maxX = 0.0f;

    public float minY = 0.0f;

    public float maxY = 0.0f;
}

[System.Serializable]
public class EnemyKinds
{
    public GameObject enemyObject;
}

public class BattleEvent : MonoBehaviour
{
    [SerializeField] public RandomAppearPosition appearPosition;

    [SerializeField] public EnemyKinds[] enemyKinds;

    [SerializeField] private int appearNumber = 0;

    
    public Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(appearPosition.minX, appearPosition.maxX);

        float randomY = Random.Range(appearPosition.minY, appearPosition.maxY);

        return new Vector3(randomX, randomY, 0.0f);
    }

    public GameObject GetRandomEnemyObject()
    {
        int appearEnemyNumber= Random.Range(0, enemyKinds.Length);

        return enemyKinds[appearEnemyNumber].enemyObject;
    }

    public void AppearEnemy()
    {
        for(int i = 0; i < appearNumber; i++)
        {
            Vector3 randomPosition = GetRandomPosition();

            GameObject enemy = GetRandomEnemyObject();

            Instantiate(enemy, randomPosition, Quaternion.identity);
        }
    }
}
