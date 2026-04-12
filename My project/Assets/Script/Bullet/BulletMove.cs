using System;
using UnityEngine;
using UnityEngine.Events;

public class BulletMove : MonoBehaviour
{
    private Vector2 direction;
    private float speed = 0.0f;
    private float range = 0.0f;

    private float traveledDistance = 0.0f;

    public Action<BulletMove> onRelease;


    public void Init(Vector2 dir, float spd, float rng)
    {
        direction = dir;
        speed = spd;
        range = rng;

        traveledDistance = 0f;
    }

    void Update()
    {
        float move = speed * Time.deltaTime;

        transform.position += (Vector3)(direction * move);
        traveledDistance += move;

        if (traveledDistance >= range)
        {
            // Poolへ返却してほしいことを通知
            //onRelease?.Invoke(this);
            BulletRelease();
        }
    }

    public void BulletRelease()
    {
        // Poolへ返却してほしいことを通知
        onRelease?.Invoke(this);
    }

    // Poolから戻すとき用に初期化
    public void ResetBullet()
    {
        traveledDistance = 0f;
    }
}
