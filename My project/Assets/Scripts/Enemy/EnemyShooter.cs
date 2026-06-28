using UnityEngine;

/// <summary>
/// 敵の射撃クラス
/// </summary>
public class EnemyShooter : MonoBehaviour
{
    [Header("敵弾プレハブ")]
    [SerializeField] private EnemyBullet bulletPrefab = null;

    [Header("発射パターン")]
    [SerializeField] private EnemyShotType shotType = EnemyShotType.Down;

    [Header("発射位置")]
    [SerializeField] private Transform shotPoint = null;

    [Header("発射間隔")]
    [SerializeField] private float shotInterval = 2.0f;

    //発射タイマー
    private float shotTimer = 0.0f;

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //タイマー更新
        shotTimer += Time.deltaTime;

        //発射間隔経過
        if (shotTimer >= shotInterval)
        {
            Shoot();

            //タイマーリセット
            shotTimer = 0f;
        }
    }

    /// <summary>
    /// 弾を発射する処理
    /// </summary>
    private void Shoot()
    {
        if (shotPoint == null)
        {
            return;
        }

        switch (shotType)
        {
            case EnemyShotType.Down:
                ShootDown();
                break;
        }
    }

    /// <summary>
    /// 真下へ1発発射
    /// </summary>
    private void ShootDown()
    {
        ObjectPool pool = GetPool();

        GameObject obj = PoolManager.Instance.EnemyBulletPool[0].Get();

        obj.transform.position = shotPoint.position;

        EnemyBullet bullet = obj.GetComponent<EnemyBullet>();
        bullet.SetDirection(Vector2.down);
    }

    private ObjectPool GetPool()
    {
        return PoolManager.Instance.EnemyBulletPool[0]; //今は仮
    }
}