using UnityEngine;

public class ReinforcementController : MonoBehaviour
{
    [SerializeField] private int currentReinforcementNumber = 0;

    [SerializeField] private int maxReinforcementNumber = 0;

    [SerializeField] private int currentLevel = 1;

    [SerializeField] private LevelUpUIManager levelUpUIManager;

    private void OnEnable()
    {
        EnemyLife.OnAddReinforcementNumber += AddGauge;
    }

    private void OnDisable()
    {
        EnemyLife.OnAddReinforcementNumber -= AddGauge;
    }


    private void AddGauge(int value)
    {
        Debug.Log("強化ゲージ加算");
        currentReinforcementNumber += value;

        if (currentReinforcementNumber >= maxReinforcementNumber)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        Debug.Log("レベルアップ");
        currentReinforcementNumber = 0;

        maxReinforcementNumber += 10;

        levelUpUIManager.OpenLevelUpUI();
    }

    private void PlayerPowerUp()
    {
        Debug.Log("プレイヤー強化");
    }
}
