using UnityEngine;

/// <summary>
/// オブジェクトプールから生成されたオブジェクトを管理するクラス
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
        //所属するプールが設定されていない場合は設定ミス
        if (pool == null)
        {
            Debug.LogError($"{name}:Poolが未設定です。");
            Destroy(gameObject);
            return;
        }

        //所属するプールへ返却する
        pool.Release(gameObject);
    }
}