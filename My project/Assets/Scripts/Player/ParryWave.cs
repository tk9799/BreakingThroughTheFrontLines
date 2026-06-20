using UnityEngine;

/// <summary>
/// ƒpƒŠƒB”g“®ƒNƒ‰ƒX
/// </summary>
public class ParryWave : MonoBehaviour
{
    [Header("Šg‘å‘¬“x")]
    [SerializeField] private float expandSpeed = 10.0f;

    [Header("Å‘åƒTƒCƒY")]
    [SerializeField] private float maxScale = 5.0f;

    /// <summary>
    /// XVˆ—
    /// </summary>
    private void Update()
    {
        //Šg‘å
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;

        //Å‘åƒTƒCƒY“’B‚Åíœ
        if (transform.localScale.x >= maxScale)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// “G’e‚ÉG‚ê‚½‚Ìˆ—
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit : " + collision.name);

        //’e‚ğæ“¾
        BulletBase bullet = collision.GetComponent<BulletBase>();

        //’e‚¶‚á‚È‚¢
        if (bullet == null)
        {
            return;
        }

        Debug.Log("Owner : " + bullet.Owner);

        //“G’e‚Ü‚½‚Íƒ{ƒX’e‚È‚çíœ
        if (bullet.Owner == BulletOwner.Enemy ||
            bullet.Owner == BulletOwner.Boss)
        {
            Debug.Log("“G’eíœ");
            Destroy(bullet.gameObject);
        }
    }
}