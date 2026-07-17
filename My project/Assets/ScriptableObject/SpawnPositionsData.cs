using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObject/SpawnPosition")]
public class SpawnPositionsData : ScriptableObject
{
    public Vector2[] spawnPosition;

    public int enemyCount;
}
