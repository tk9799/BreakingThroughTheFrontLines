using UnityEngine;

/// <summary>
/// “G‚ÌŠî’êƒNƒ‰ƒX
/// </summary>
public abstract class EnemyBase : MonoBehaviour
{
    [Header("“G‚ÌÅ‘åHP")]
    [SerializeField] private int maxHp = 10;

    //Œ»İHP
    private int currentHp = 0;

    /// <summary>
    /// Å‘åHP
    /// </summary>
    public int MaxHp => maxHp;

    /// <summary>
    /// Œ»İHP
    /// </summary>
    public int CurrentHp => currentHp;

    /// <summary>
    /// ‰Šú‰»ˆ—
    /// </summary>
    protected virtual void Awake()
    {
        //Œ»İHP‚ğÅ‘åHP‚Å‰Šú‰»
        currentHp = maxHp;
    }

    /// <summary>
    /// ƒ_ƒ[ƒWˆ—
    /// </summary>
    public virtual void Damage(int damage)
    {
        //Œ»İHP‚ğŒ¸‚ç‚·
        currentHp -= damage;

        //HP‚ª0ˆÈ‰º‚È‚ç€–S
        if (currentHp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// €–Sˆ—
    /// </summary>
    protected virtual void Die()
    {
        //“G‚ğíœ
        Destroy(gameObject);
    }
}