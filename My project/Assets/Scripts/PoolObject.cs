using UnityEngine;
using System.Collections;

/// <summary>
/// プールから生成されたオブジェクトを管理するクラス
/// </summary>
public class PoolObject : MonoBehaviour
{
    //所属しているオブジェクトプール
    private ObjectPool pool = null;

    /// <summary>
    /// 所属するオブジェクトプールを設定する処理
    /// </summary>
    public void SetPool(ObjectPool objectPool)
    {
        pool = objectPool;
    }

    /// <summary>
    /// 自分自身をプールへ返却する処理
    /// </summary>
    public void Release()
    {
        //
        if (pool == null)
        {
            Debug.LogWarning($"{name}: Poolが未設定です");
            Destroy(gameObject);
            return;
        }

        //
        pool.Release(gameObject);
    }
}