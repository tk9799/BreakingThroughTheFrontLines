using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [Header("飛ばす弾オブジェクト")]
    [SerializeField] GameObject bulletPrefab;

    [Header("飛ばす弾の位置")]
    [SerializeField] private Transform[] firePoints;

    [Header("弾幕を発射する数")]
    [SerializeField] public int currentFireCount = 0;

    public int maxFireCount => firePoints.Length;

    [Header("弾の速度")]
    [SerializeField] float bulletSpeed = 10f;

    [Header("マウスの方向を取得するクラス")]
    [SerializeField] GetMouseDirection getMouseDirection;

    [Header("射程")]
    [SerializeField] public float bulletRange = 0.0f;

    [Header("ダメージの初期値")]
    [SerializeField] private int initialDamageValue = 0;

    [Header("弾のダメージ")]
    [SerializeField] public int currentDamage = 0;

    // 連射速度
    [Header("射撃後のクールタイム")]
    [SerializeField] public float fireInterval = 0.2f;

    [Header("発射間隔")]
    [SerializeField] private float shotSpacing = 0.5f;

    [Header("弾を扇状で飛ばす際の角度")]
    [SerializeField] private float spreadAngle = 0.0f;

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

        currentDamage = initialDamageValue;

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
            FanShaped();
            timer = 0f;
        }
    }

    

    /// <summary>
    /// 扇状の弾幕を飛ばす処理
    /// </summary>
    private void FanShaped()
    {
        for (int i = 0; i < currentFireCount; i++)
        {
            if (i >= firePoints.Length) break;

            BulletMove bulletMove = bulletPool.Get();

            BulletDamage bulletDamage = bulletMove.GetComponent<BulletDamage>();

            bulletDamage.damage = currentDamage;

            // 発射方向に対して垂直なベクトル
            // 発射位置に対して横方向に横方向になり左右に並ぶようになる
            Vector2 perpendicuar = new Vector2(-direction.y, direction.x);

            // 中心基準に並べる
            // 発射位置をずらす処理
            // 処理がループするたびに位置が変わる
            float offset = (i - (currentFireCount - 1) * 0.5f)
                * shotSpacing;

            // 飛ばす位置を回転させる処理
            float angle = (i - (currentFireCount - 1) * 0.5f) * spreadAngle;

            // angleの数だけ回転させる
            Vector2 shotDirection = Quaternion.Euler(0.0f, 0.0f, angle) * direction;

            // 発射位置
            Vector2 spawnPosition = (Vector2)transform.position + perpendicuar
                * offset;

            bulletMove.transform.position = spawnPosition;

            // 弾の返却イベントを設定
            bulletMove.onRelease -= bulletPool.Release;
            bulletMove.onRelease -= BulletDamageInitialization;

            bulletMove.onRelease += bulletPool.Release;
            bulletMove.onRelease += BulletDamageInitialization;

            // 弾を飛ばす際に必要な情報を弾の移動スクリプトに渡す
            // 扇状に飛ばすためdirectionを回転させたshotDirectionを渡す
            bulletMove.Init(shotDirection, bulletSpeed, bulletRange);
        }
    }

    private void BulletDamageInitialization(BulletMove bulletMove)
    {
        BulletDamage bulletDamage = bulletMove.GetComponent<BulletDamage>();

        bulletDamage.damage = 0;
    }

    /// <summary>
    /// RayをScene画面に可視化するためのメソッド
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

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
