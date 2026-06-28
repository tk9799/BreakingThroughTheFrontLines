using UnityEngine;
using System.Collections;

/// <summary>
/// ’e‚ÌŠî’êƒNƒ‰ƒX
/// </summary>
public abstract class BulletBase : MonoBehaviour
{
    [Header("’e‚ÌŠ‘®")]
    [SerializeField] protected BulletOwner owner = BulletOwner.None;

    [Header("’e‘¬")]
    [SerializeField] protected float speed = 5.0f;

    [Header("¶‘¶ŠÔ")]
    [SerializeField] protected float lifeTime = 3.0f;

    //’e‚ÌˆÚ“®•ûŒü
    protected Vector2 direction = Vector2.zero;

    //
    private Coroutine lifeCoroutine = null;

    //
    private PoolObject poolObj = null;

    /// <summary>
    /// ’e‚ÌŠ‘®
    /// </summary>
    public BulletOwner Owner => owner;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        //
        poolObj = GetComponent<PoolObject>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        //
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
        }

        //
        lifeCoroutine = StartCoroutine(LifeRoutine());
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        //
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
            lifeCoroutine = null;
        }
    }

    /// <summary>
    /// XVˆ—
    /// </summary>
    protected virtual void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    /// <summary>
    /// ’e‚ÌˆÚ“®•ûŒü‚ğİ’è
    /// </summary>
    public virtual void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }

    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Despawn();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Despawn()
    {
        //
        if (poolObj != null)
        {
            poolObj.Release();
        }
        else
        {
            GetComponent<PoolObject>()?.Release();
        }
    }
}