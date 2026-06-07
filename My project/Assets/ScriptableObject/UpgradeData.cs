using UnityEngine;

[CreateAssetMenu(menuName ="Upgrade/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    // 強化の名前
    public string upgradeName;

    // 何を強化するかの説明
    public string description;

    // 強化の種類
    public UpgradeType upgradeType;

    // 強化する値
    public int upgradeValue;

    public float upgradeValueFloat;
}
