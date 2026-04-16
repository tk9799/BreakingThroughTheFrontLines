using System;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // 弾の移動方向
    private Vector2 bulletDirection;

    //
    private float bulletMoveSpeed = 0.0f;
    private float bulletRange = 0.0f;

    private float traveledDistance = 0.0f;

    public Action<BulletMove> onRelease;

    /// <summary>
    /// 弾を飛ばすのに必要な情報を代入するメソッド
    /// </summary>
    public void Init(Vector2 direction, float speed, float range)
    {
        // 呼び出し先で弾の方向、スピード、射程を設定する
        bulletDirection = direction;
        bulletMoveSpeed = speed;
        bulletRange = range;

        traveledDistance = 0f;
    }

    private void Update()
    {
        // 弾を移動させる数値を計算
        float move = bulletMoveSpeed * Time.deltaTime;

        // 弾の移動
        transform.position += (Vector3)(bulletDirection * move);
        traveledDistance += move;

        // 弾の射程を超えたらPoolに返却
        if (traveledDistance >= bulletRange)
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
}
