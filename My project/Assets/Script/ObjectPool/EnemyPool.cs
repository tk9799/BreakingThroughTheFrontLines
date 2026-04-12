using System.Collections.Generic;
using UnityEngine;

public class EnemyPool<T> where T : Component
{
    private T enemyPrefab = null;
    private Queue<T> pool = new Queue<T>();
    private Transform parent = null;
    public EnemyPool(T enemyPrefab, int initialSize, Transform parent = null)
    {
        this.enemyPrefab = enemyPrefab;
        this.parent = parent; for (int i = 0;
            i < initialSize; i++)
        {
            T enemyObject = CreateNewObject();
            pool.Enqueue(enemyObject);
        }
    }
    private T CreateNewObject()
    {
        T enemyObject = GameObject.Instantiate(enemyPrefab, parent);
        enemyObject.gameObject.SetActive(false); return enemyObject;
    }
    public T Get()
    {
        if (pool.Count > 0)
        {
            T enemyObject = pool.Dequeue();
            enemyObject.gameObject.SetActive(true);
            return enemyObject;
        }
        else
        {
            return CreateNewObject();
        }
    }
    public void Release(T enemyObject)
    {
        enemyObject.gameObject.SetActive(false);
        pool.Enqueue(enemyObject);
    }
}
