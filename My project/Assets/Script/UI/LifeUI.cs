using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [Header("Lifeアイコンのプレハブ")]
    [SerializeField] private GameObject LifeObjectPrefab;

    [Header("アイコンを配置する親オブジェクト")]
    [SerializeField] private Transform iconParent;
    [SerializeField] private EnemyHitJudgement enemyHitJudgement;

    private GameObject[] lifeIcons;   // ← 生成したアイコンを保存

    private int currentLifeCount = 0;

    void Start()
    {
        int maxLife = enemyHitJudgement.myLife;
        currentLifeCount = maxLife;

        // アイコン配列を作成
        lifeIcons = new GameObject[maxLife];

        // 必要個数分のアイコンを生成
        for (int i = 0; i < maxLife; i++)
        {
            GameObject icon = Instantiate(LifeObjectPrefab, iconParent);
            lifeIcons[i] = icon;
        }
    }

    void Update()
    {
        int updateLifeCount = enemyHitJudgement.myLife;

        if (updateLifeCount != currentLifeCount)
        {
            UpdateLifeUI(updateLifeCount);
            currentLifeCount = updateLifeCount;
        }
    }

    /// <summary>
    /// ライフUIの表示数を変えるメソッド
    /// </summary>
    private void UpdateLifeUI(int count)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            // count = 残りライフ
            bool isActive = (i < count);
            lifeIcons[i].SetActive(isActive);
        }
    }
}
