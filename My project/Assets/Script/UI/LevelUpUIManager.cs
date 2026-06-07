using TMPro;
using UnityEngine;

public class LevelUpUIManager : MonoBehaviour
{
    [Header("レベルアップUIのパネル")]
    [SerializeField] private GameObject levelUpPanel;

    [Header("強化項目のデータ")]
    [SerializeField] private UpgradeData[] upgradeDatas;

    [Header("強化項目のテキスト")]
    [SerializeField] private TMP_Text[] buttonTexts;

    // 強化する選択肢の数
    private UpgradeData[] currentChoices = new UpgradeData[3];

    [SerializeField] private PlayerMove playerMove;

    //[SerializeField] private BulletDamage bulletDamage;

    [SerializeField] private ShootBullet shootBullet;

    /// <summary>
    /// レベルアップUIを開く
    /// 強化するステータスの項目をランダムに決める
    /// </summary>
    public void OpenLevelUpUI()
    {
        levelUpPanel.SetActive(true);

        GenerateChoices();

        // ゲームを一時停止する
        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// レベルアップUIを閉じる
    /// </summary>
    public void CloseLevelUpUI()
    {
        levelUpPanel.SetActive(false);

        // ゲームを再開する
        Time.timeScale = 1.0f;
    }

    private UpgradeData GetRandomUpgrade()
    {
        int index = Random.Range(0, upgradeDatas.Length);

        return upgradeDatas[index];
    }

    /// <summary>
    /// 強化するステータスの項目をランダムに決めたものをUIに反映させる
    /// </summary>
    private void GenerateChoices()
    {
        for(int i=0;i< currentChoices.Length; i++)
        {
            currentChoices[i] = GetRandomUpgrade();
            buttonTexts[i].text = currentChoices[i].description;

            Debug.Log($"選択肢{i + 1}: {currentChoices[i].description}");
        }
    }

    public void SelectUpgrade(int index)
    {
        UpgradeData selectData = currentChoices[index];

        ApplyUpgrade(selectData);

        CloseLevelUpUI();
    }

    // 弾幕を飛ばす方向を増やす処理が上手くいっていない
    private void ApplyUpgrade(UpgradeData data)
    {
        switch (data.upgradeType)
        {
            case UpgradeType.MoveSpeed:
                
                playerMove.moveSpeed += data.upgradeValue;
                Debug.Log("移動速度UP");
                break;

            case UpgradeType.AttackPower:

                shootBullet.currentDamage += data.upgradeValue;
                Debug.Log("攻撃力UP");
                break;

            case UpgradeType.BulletRange:
                shootBullet.bulletRange += data.upgradeValueFloat;
                Debug.Log("弾の射程UP");
                break;

            case UpgradeType.BulletCount:
                if (shootBullet.currentFireCount < shootBullet.maxFireCount)
                {
                    shootBullet.currentFireCount += data.upgradeValue;
                    Debug.Log("弾幕を飛ばす方向UP");
                }
                else if(shootBullet.currentFireCount >= shootBullet.maxFireCount)
                {
                    shootBullet.currentDamage += data.upgradeValue;
                    Debug.Log("弾幕を飛ばす方向を追加できないので攻撃力UP");
                }
                break;

            case UpgradeType.FireRate:
                shootBullet.fireInterval=shootBullet.fireInterval
                    / data.upgradeValueFloat;
                Debug.Log("攻撃速度UP");
                break;
        }
    }
}
