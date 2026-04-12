using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [Header("飛ばす弾丸オブジェクト")]
    [SerializeField] GameObject bulletPrefab;

    [Header("飛ばす弾丸の位置")]
    [SerializeField] private Transform firePoint; 
    [SerializeField] float bulletSpeed = 10f; 
    [SerializeField] GetMouseDirection getMouseDirection;

    [Header("射程")]
    [SerializeField] private float bulletRange = 0.0f;

    [Header("射撃後のクールタイム")]
    [SerializeField] float fireInterval = 0.2f;
    private float timer = 0.0f;

    private Vector2 direction;

    [SerializeField] LineRenderer lineRender;

    private BulletPool<BulletMove> bulletPool = null;
    private List<BulletMove> bulletList = new List<BulletMove>(); 
    private BulletMove bulletMove = null;

    private void Start() 
    {
        bulletPool = new BulletPool<BulletMove>
            (bulletPrefab.GetComponent<BulletMove>(), 100, transform);

        //LineRenderの線の太さ
        lineRender.startWidth = 0.04f; 
        lineRender.endWidth = 0.04f;

        bulletMove = GetComponent<BulletMove>(); 
    }

    private void Update() 
    {
        direction = getMouseDirection.MouseDirection();
        Physics2D.Raycast(transform.position, direction, bulletRange);
        // Ray可視化
        lineRender.SetPosition(0, transform.position);

        lineRender.SetPosition(1, (Vector2)transform.position + direction * bulletRange);

        timer += Time.deltaTime;

        if (timer >= fireInterval)
        {
            Shoot();
            timer = 0f;
        }
    }
    private void Shoot() 
    {
        
        
        BulletMove bulletMove = bulletPool.Get();

        bulletMove.transform.position = firePoint.position;

        // 弾の返却イベントを設定（ここが重要！）
        bulletMove.onRelease = bulletPool.Release;

        // 初期化
        bulletMove.Init(direction, bulletSpeed, bulletRange);
    }

    //void DrawRay()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, bulletRange);

    //    Vector2 endPoint = hit.collider != null
    //        ? hit.point
    //        : (Vector2)transform.position + direction * bulletRange;

    //    lineRender.SetPosition(0, transform.position);

    //    lineRender.SetPosition(1, (Vector2)transform.position + direction * bulletRange);
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // 再生中じゃなくても方向を取得
        Vector2 dir = getMouseDirection != null
            ? getMouseDirection.MouseDirection()
            : Vector2.right;

        Gizmos.DrawRay(transform.position, dir * bulletRange);
    }
}
