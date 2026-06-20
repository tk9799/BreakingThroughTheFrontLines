using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーのパリィクラス
/// </summary>
public class PlayerParry : MonoBehaviour
{
    [Header("パリィ判定範囲")]
    [SerializeField] private ParryArea parryArea = null;

    [Header("パリィ波動プレハブ")]
    [SerializeField] private ParryWave parryWavePrefab = null;

    [Header("クールタイム時間")]
    [SerializeField] private float cooldown = 5.0f;

    //クールタイム残り時間
    private float currentCooldown = 0f;

    //クールタイム中かどうか
    private bool isCooldown = false;

    /// <summary>
    /// クールタイム時間
    /// </summary>
    public float Cooldown => cooldown;

    /// <summary>
    /// クールタイム残り時間
    /// </summary>
    public float CurrentCooldown => currentCooldown;

    /// <summary>
    /// クールタイム中かどうか
    /// </summary>
    public bool IsCooldown => isCooldown;

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        //クールタイム中は使用不可
        if (isCooldown)
        {
            return;
        }

        //パリィ入力
        if (Input.GetKeyDown(KeyCode.E))
        {
            Parry();
        }
    }

    /// <summary>
    /// パリィ処理
    /// </summary>
    private void Parry()
    {
        //
        if (parryArea.HasEnemyBullet)
        {
            Debug.Log("パリィ成功");

            //パリィ波動を生成
            Instantiate(parryWavePrefab, transform.position, Quaternion.identity);

            //クールタイム開始
            StartCoroutine(CooldownCoroutine());
        }
        else
        {
            Debug.Log("パリィ失敗");
            return;
        }
    }

    /// <summary>
    /// クールタイム処理
    /// </summary>
    private IEnumerator CooldownCoroutine()
    {
        //クールタイム開始
        isCooldown = true;

        //残り時間を設定
        currentCooldown = cooldown;

        while (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
            yield return null;
        }

        //クールタイム終了
        currentCooldown = 0f;
        isCooldown = false;

        Debug.Log("クールタイム終了");
    }
}