using UnityEngine;

/// <summary>
/// ’e‚ÌŠî’êƒNƒ‰ƒX
/// </summary>
public class BulletBase : MonoBehaviour
{
    [Header("’e‚ÌŠ‘®")]
    [SerializeField] protected BulletOwner owner = BulletOwner.None;

    [Header("’e‘¬")]
    [SerializeField] protected float speed = 5.0f;

    [Header("¶‘¶ŠÔ")]
    [SerializeField] protected float lifeTime = 3.0f;

    //’e‚ÌˆÚ“®•ûŒü
    protected Vector2 direction = Vector2.zero;

    /// <summary>
    /// ’e‚ÌŠ‘®
    /// </summary>
    public BulletOwner Owner => owner;

    /// <summary>
    /// ‰Šú‰»ˆ—
    /// </summary>
    protected virtual void Start()
    {
        Destroy(gameObject, lifeTime);
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
}