using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [Header("飛ばす弾オブジェクト")]
    [SerializeField] GameObject bulletPrefab;

    [Header("飛ばす弾の位置")]
    [SerializeField] private Transform firePoint;

    [Header("弾の速度")]
    [SerializeField] float bulletSpeed = 10f;

    [Header("マウスの方向を取得するクラス")]
    [SerializeField] GetMouseDirection getMouseDirection;

    [Header("射程")]
    [SerializeField] private float bulletRange = 0.0f;

    [Header("射撃後のクールタイム")]
    [SerializeField] float fireInterval = 0.2f;

    // 連射時に使うタイマー
    private float timer = 0.0f;

    // 弾を飛ばす方向を決める際に使うVector2型の変数
    private Vector2 direction;

    [Header("LineRenderがついているオブジェクトをアタッチ")]
    [SerializeField] LineRenderer lineRender;

    // 弾のオブジェクトプールと、弾のリスト、弾の移動スクリプトを格納する変数
    private BulletPool<BulletMove> bulletPool = null;

    private void Start() 
    {
        // 弾のオブジェクトプールを作成
        // 引数に弾のプレハブ、プールに生成する数、生成元を渡す
        bulletPool = new BulletPool<BulletMove>
            (bulletPrefab.GetComponent<BulletMove>(), 100, transform);

        //LineRenderの線の太さ
        lineRender.startWidth = 0.04f; 
        lineRender.endWidth = 0.04f;
    }

    private void Update() 
    {
        // マウスの方向を代入する
        direction = getMouseDirection.MouseDirection();

        // このオブジェクトの座標からdirectionの方向にbulletRangeの距離だけRayを飛ばす
        Physics2D.Raycast(transform.position, direction, bulletRange);

        // Ray可視化
        // LineRenderの始点をこのオブジェクトの座標に設定
        lineRender.SetPosition(0, transform.position);

        // directionの方向にbulletRangeの距離だけRayを飛ばす
        lineRender.SetPosition(1, (Vector2)transform.position + direction * bulletRange);

        // タイマーの時間を起動からの経過時間にする
        timer += Time.deltaTime;

        // タイマーがfireInterval以上になったらShoot()を呼び出して、タイマーを0にする
        if (timer >= fireInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    /// <summary>
    /// 弾を発射する際に呼び出されるメソッド
    /// </summary>
    private void Shoot() 
    {
        // 弾のオブジェクトプールから弾を取得
        BulletMove bulletMove = bulletPool.Get();

        // 弾の位置をfirePointに設定
        bulletMove.transform.position = firePoint.position;

        // 弾の返却イベントを設定
        bulletMove.onRelease = bulletPool.Release;

        // 弾を飛ばす際に必要な情報を弾の移動スクリプトに渡す
        bulletMove.Init(direction, bulletSpeed, bulletRange);
    }

    /// <summary>
    /// RayをScene画面に可視化するためのメソッド
    /// </summary>
    private void OnDrawGizmos()
    {
        // Gizmosの色を赤に設定
        Gizmos.color = Color.red;

        // 再生中じゃなくても方向を取得
        Vector2 dir = getMouseDirection != null
            ? getMouseDirection.MouseDirection()
            : Vector2.right;

        // このオブジェクトの方向からdirの方向にbulletRangeの距離だけRayを飛ばす
        Gizmos.DrawRay(transform.position, dir * bulletRange);
    }
}
