using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 同じプレハブを再利用するためのオブジェクトプール
/// </summary>
public class ObjectPool : MonoBehaviour
{
    [Header("プールするプレハブ")]
    [SerializeField] private GameObject poolPrefab = null;

    [Header("初期生成数")]
    [SerializeField, Min(1)] private int defaultSize = 0;

    //未使用オブジェクトを保持するプール
    private readonly Queue<GameObject> pool = new();

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //プレハブ未設定なら処理しない
        if (poolPrefab == null)
        {
            Debug.LogError($"{name}:PoolPrefabが設定されていません");
            enabled = false;
            return;
        }

        //初期数だけ生成
        for (int i = 0; i < defaultSize; i++)
        {
            CreateObject();
        }
    }

    /// <summary>
    /// オブジェクトを取得する処理
    /// </summary>
    public GameObject Get()
    {
        //足りなければ追加生成
        if (pool.Count == 0)
        {
            if (CreateObject() == null)
            {
                return null;
            }
        }

        //取り出す
        GameObject obj = pool.Dequeue();

        //親を外す
        obj.transform.SetParent(null);

        //Transformを初期状態に戻す
        obj.transform.localScale = Vector3.one;
        obj.transform.rotation = Quaternion.identity;

        //有効化
        obj.SetActive(true);

        return obj;
    }

    /// <summary>
    /// オブジェクトを生成する処理
    /// </summary>
    private GameObject CreateObject()
    {
        //生成
        GameObject obj = Instantiate(poolPrefab, transform);

        //PoolObjectを取得
        PoolObject poolObject = obj.GetComponent<PoolObject>();

        //PoolObjectが無い場合はプールできない
        if (poolObject == null)
        {
            Debug.LogError($"{poolPrefab.name}にPoolObjectがアタッチされていません。");
            Destroy(obj);
            return null;
        }

        //自分が所属するプールを登録
        poolObject.SetPool(this);

        //使用待ち状態にする
        obj.SetActive(false);

        //プールへ追加
        pool.Enqueue(obj);

        return obj;
    }

    /// <summary>
    /// オブジェクトを返却する処理
    /// </summary>
    public void Release(GameObject obj)
    {
        //既に返却済みなら何もしない
        if (!obj.activeSelf)
        {
            Debug.LogWarning($"{obj.name}は既にプールへ返却されています。");
            return;
        }

        //オブジェクトを非表示にする
        obj.SetActive(false);

        //プールの子に戻す
        obj.transform.SetParent(transform);

        //Transformを初期状態に戻す
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;

        //プールへ戻す
        pool.Enqueue(obj);
    }
}