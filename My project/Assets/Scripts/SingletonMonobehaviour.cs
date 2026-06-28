using UnityEngine;
using System;

/// <summary>
/// MonoBehaviour用のシングルトン基底クラス
/// 継承することで、そのクラスをゲーム全体で1つだけ存在させることができる
/// </summary>
public abstract class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// シングルトンインスタンス
    /// </summary>
    private static T instance;

    /// <summary>
    /// シーンを跨いで破棄しないかどうか
    /// </summary>
    [SerializeField] private bool dontDestroyOnLoad = true;

    /// <summary>
    /// シングルトンインスタンスへのグローバルアクセスポイント
    /// まだ生成されていない場合はシーン内から検索して取得する
    /// </summary>
    public static T Instance
    {
        //シングルトンインスタンスへアクセスするためのプロパティ
        get
        {
            //すでに存在しているならそれを返す
            if (instance != null) return instance;

            //型情報を取得
            Type type = typeof(T);

            //シーン内から該当コンポーネントを検索して取得
            instance = FindFirstObjectByType<T>();

            //見つからなかった場合はエラー表示
            if (instance == null)
            {
                Debug.LogError(type + "をアタッチしているGameObjectはありません");
            }

            return instance;
        }
    }

    /// <summary>
    /// Unityの初期化処理
    /// 既にインスタンスが存在する場合は自分を破棄し、
    /// 存在しない場合はこのオブジェクトをシングルトンとして登録する
    /// </summary>
    protected virtual void Awake()
    {
        Debug.Log($"{typeof(T)} Awake");

        //すでに別のインスタンスが存在している場合は自分を破棄
        if (instance != null && instance != this)
        {
            Debug.LogWarning($"重複するシングルトンが破棄されました:{typeof(T)}");
            Destroy(gameObject);
            return;
        }

        //自分をシングルトンインスタンスとして登録
        instance = this as T;

        //シーン跨ぎ制御
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}