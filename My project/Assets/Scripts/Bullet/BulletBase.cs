using UnityEngine;
using System.Collections;

/// <summary>
/// 弾の共通処理を管理する基底クラス
/// </summary>
[RequireComponent(typeof(PoolObject))]
public abstract class BulletBase : MonoBehaviour
{
    [Header("弾の所属")]
    [SerializeField] protected BulletOwner owner = BulletOwner.None;

    [Header("弾速")]
    [SerializeField] protected float speed = 5.0f;

    [Header("生存時間")]
    [SerializeField] protected float lifeTime = 3.0f;

    //弾の移動方向
    protected Vector2 direction = Vector2.zero;

    //生存時間を管理するコルーチン
    private Coroutine lifeCoroutine = null;

    //自身をプールへ返却するためのコンポーネント
    private PoolObject poolObj = null;

    /// <summary>
    /// 弾の所属
    /// </summary>
    public BulletOwner Owner => owner;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //PoolObjectを取得
        poolObj = GetComponent<PoolObject>();

        //PoolObjectが取得できなければ処理を停止
        if (poolObj == null)
        {
            Debug.LogError($"{name}: PoolObjectが取得できません。");
            enabled = false;
            return;
        }
    }

    /// <summary>
    /// 弾が有効化されたときの処理
    /// </summary>
    private void OnEnable()
    {
        //多重実行を防ぐため既存のコルーチンを停止
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
        }

        //生存時間のカウントを開始
        lifeCoroutine = StartCoroutine(LifeRoutine());
    }

    /// <summary>
    /// 弾が無効化されたときの処理
    /// </summary>
    private void OnDisable()
    {
        //実行中のコルーチンを停止
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
            lifeCoroutine = null;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    protected virtual void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    /// <summary>
    /// 弾の移動方向を設定
    /// </summary>
    public virtual void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }

    /// <summary>
    /// 生存時間を管理するコルーチン
    /// </summary>
    private IEnumerator LifeRoutine()
    {
        //一定時間経過後にプールへ返却
        yield return new WaitForSeconds(lifeTime);
        Despawn();
    }

    /// <summary>
    /// 弾をプールへ返却する処理
    /// </summary>
    protected void Despawn()
    {
        //プールへ返却
        poolObj.Release();
    }
}