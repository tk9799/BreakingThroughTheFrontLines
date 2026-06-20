using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーの状態管理クラス
/// </summary>
public class PlayerStatus : MonoBehaviour
{
    [Header("残機")]
    [SerializeField] private int life = 3;

    [Header("無敵時間")]
    [SerializeField] private float invincibilityTime = 3.0f;

    //無敵状態かどうか
    private bool isInvincible = false;

    //SpriteRendererコンポーネント
    private SpriteRenderer spriteRenderer = null;

    //
    public bool IsParryActive { get; private set; }

    /// <summary>
    /// 残機
    /// </summary>
    public int Life => life;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //コンポーネント取得
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    public void Damage()
    {
        //無敵中はダメージを受けない
        if (isInvincible)
        {
            return;
        }

        //残機を減らす
        life--;
        Debug.Log($"残機:{life}");

        //無敵時間開始
        StartCoroutine(InvincibilityCoroutine());

        //残機が0以下になったらゲームオーバー
        if (life <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// 無敵時間処理
    /// </summary>
    private IEnumerator InvincibilityCoroutine()
    {
        //無敵状態にする
        isInvincible = true;

        //無敵時間の経過時間
        float elapsedTime = 0f;

        //無敵時間が終了するまで点滅を繰り返す
        while (elapsedTime < invincibilityTime)
        {
            //表示切り替え
            spriteRenderer.enabled = !spriteRenderer.enabled;

            //点滅間隔
            yield return new WaitForSeconds(0.1f);

            //経過時間を加算
            elapsedTime += 0.1f;
        }

        //表示状態を戻す
        spriteRenderer.enabled = true;

        //無敵状態を解除
        isInvincible = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void SetParry(bool value)
    {
        IsParryActive = value;
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    private void GameOver()
    {
        Debug.Log("ゲームオーバー");
    }
}