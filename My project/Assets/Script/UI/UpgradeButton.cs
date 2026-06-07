using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;

    private UpgradeData upgradeData;

    public void Setup(UpgradeData data)
    {
        upgradeData = data;

        buttonText.text = upgradeData.description;
    }
    
}
