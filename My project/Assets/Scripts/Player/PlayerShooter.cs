using UnityEngine;

/// <summary>
/// プレイヤーの射撃クラス
/// </summary>
public class PlayerShooter : MonoBehaviour
{
    [Header("弾プレハブ")]
    [SerializeField] private PlayerBullet bulletPrefab = null;

    [Header("発射位置")]
    [SerializeField] private Transform shotPoint = null;

    [Header("発射間隔")]
    [SerializeField] private float shotInterval = 0.1f;

    //現在の射撃方向
    private Vector2 shotDirection = Vector2.zero;

    //発射タイマー
    private float shotTimer = 0f;

    /// <summary>
    /// 射撃方向
    /// </summary>
    public Vector2 ShotDirection => shotDirection;

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //マウス位置をワールド座標に変換
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Z座標を補正
        mousePosition.z = 0f;

        //プレイヤーからマウスへの方向を取得
        shotDirection = (mousePosition - transform.position).normalized;

        //発射タイマーを更新
        shotTimer += Time.deltaTime;

        //左クリック押下中
        if (Input.GetMouseButton(0))
        {
            //発射間隔経過
            if (shotTimer >= shotInterval)
            {
                Shoot();

                //タイマーをリセット
                shotTimer = 0f;
            }
        }
    }

    /// <summary>
    /// 弾を発射する処理
    /// </summary>
    private void Shoot()
    {
        //
        GameObject obj = PoolManager.Instance.GetPlayerBullet();

        //
        if (obj == null)
        {
            return;
        }

        //
        obj.transform.position = shotPoint.position;

        //
        PlayerBullet bullet = obj.GetComponent<PlayerBullet>();

        //
        bullet.SetDirection(shotDirection);
    }
}