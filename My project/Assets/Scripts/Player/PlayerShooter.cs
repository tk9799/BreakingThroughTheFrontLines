using UnityEngine;

/// <summary>
/// プレイヤーの射撃クラス
/// </summary>
public class PlayerShooter : MonoBehaviour
{
    [Header("弾プレハブ")]
    [SerializeField] private GameObject bulletPrefab = null;

    [Header("発射位置")]
    [SerializeField] private Transform shotPoint = null;

    [Header("発射間隔")]
    [SerializeField] private float shotInterval = 0.1f;

    //発射タイマー
    private float shotTimer = 0f;

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //発射タイマーを更新
        shotTimer += Time.deltaTime;

        //Zキー押下中
        if (Input.GetKey(KeyCode.Z))
        {
            //発射間隔経過
            if (shotTimer >= shotInterval)
            {
                //弾を生成
                Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);

                //タイマーリセット
                shotTimer = 0f;
            }
        }
    }
}