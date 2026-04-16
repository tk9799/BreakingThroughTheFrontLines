using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コンポーネントなら何でもプールできるBulletPoolクラス
/// </summary>
public class BulletPool<T> where T : Component 
{
    // テンプレート型の変数を宣言
    private T bulletPrefab = null;

    // プールを管理するキューを宣言
    private Queue<T> pool = new Queue<T>();

    // 生成したオブジェクトをまとめる親
    private Transform parent = null; 

    /// <summary>
    /// コンストラクタ
    /// 生成するオブジェクト、生成する値、生成元を引数にする
    /// </summary>
    public BulletPool(T bulletPrefab, int initialSize, Transform parent = null) 
    { 
        this.bulletPrefab = bulletPrefab; 

        // 生成する値の数までループする
        this.parent = parent; for (int i = 0; 
            i < initialSize; i++) 
        {
            // オブジェクトを生成する
            T bulletObject = CreateNewObject(); 

            // キューに登録する
            pool.Enqueue(bulletObject); 
        } 
    } 

    /// <summary>
    /// オブジェクトを生成してそのオブジェクトをT型で返すメソッド
    /// </summary>
    private T CreateNewObject() 
    {
        // オブジェクトを生成する
        T bulletObject = GameObject.Instantiate(bulletPrefab, parent); 

        // 生成したオブジェクトを非表示にする
        bulletObject.gameObject.SetActive(false);

        // 生成したオブジェクトをT型で返す
        return bulletObject; 
    } 

    /// <summary>
    /// プールからオブジェクトを取り出して表示するメソッド
    /// </summary>
    /// <returns></returns>
    public T Get() 
    {
        // キューの中にプールされたオブジェクトがあるか確認する
        if (pool.Count > 0)
        {
            // キューからオブジェクトを取り出す
            T bulletObject = pool.Dequeue();

            // 取り出したオブジェクトを表示する
            bulletObject.gameObject.SetActive(true);

            // 取り出したオブジェクトをT型で返す
            return bulletObject; 
        }
        // キューの中にプールがない場合は新しく生成する
        else 
        { 
            return CreateNewObject();
        } 
    }

    /// <summary>
    /// オブジェクトをプールに返すメソッド
    /// </summary>
    public void Release(T bulletObject) 
    {
        // オブジェクトを非表示にしてキューに登録する
        bulletObject.gameObject.SetActive(false); 
        pool.Enqueue(bulletObject); 
    } 
}
