using System.Collections.Generic;
using UnityEngine;

public class BulletPool<T> where T : Component 
{
    private T bulletPrefab = null;
    private Queue<T> pool = new Queue<T>();
    private Transform parent = null; 
    public BulletPool(T bulletPrefab, int initialSize, Transform parent = null) 
    { 
        this.bulletPrefab = bulletPrefab; 
        this.parent = parent; for (int i = 0; 
            i < initialSize; i++) 
        { 
            T bulletObject = CreateNewObject(); 
            pool.Enqueue(bulletObject); 
        } 
    } 
    private T CreateNewObject() 
    { 
        T bulletObject = GameObject.Instantiate(bulletPrefab, parent); 
        bulletObject.gameObject.SetActive(false); return bulletObject; 
    } 
    public T Get() 
    { 
        if (pool.Count > 0)
        { 
            T bulletObject = pool.Dequeue(); 
            bulletObject.gameObject.SetActive(true); 
            return bulletObject; 
        } 
        else 
        { 
            return CreateNewObject();
        } 
    }
    public void Release(T bulletObject) 
    {
        bulletObject.gameObject.SetActive(false); 
        pool.Enqueue(bulletObject); 
    } 
}
