using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [Header("Lifeアイコンのプレハブ")]
    [SerializeField] private GameObject LifeObjectPrefab;

    [Header("アイコンを配置する親オブジェクト")]
    [SerializeField] private Transform iconParent;

    [Header("EnemyHitJudgementスクリプト")]
    [SerializeField] private EnemyHitJudgement enemyHitJudgement;

    // 生成したアイコン
    private GameObject[] lifeIcons;

    // 現在のライフ数を保存する変数
    private int currentLifeCount = 0;

    void Start()
    {
        // EnemyHitJudgementから最大ライフ数を取得
        int maxLife = enemyHitJudgement.myLife;

        // 現在のライフ数を最大ライフ数で初期化
        currentLifeCount = maxLife;

        // アイコン配列を作成
        lifeIcons = new GameObject[maxLife];

        // 必要個数分のアイコンを生成
        for (int i = 0; i < maxLife; i++)
        {
            GameObject icon = Instantiate(LifeObjectPrefab, iconParent);

            // アイコンを配列に保存
            lifeIcons[i] = icon;
        }
    }

    void Update()
    {
        // EnemyHitJudgementから現在のライフ数を取得
        int updateLifeCount = enemyHitJudgement.myLife;

        // ライフ数が変わったときだけUIを更新
        if (updateLifeCount != currentLifeCount)
        {
            // ライフUIの表示数を変える
            UpdateLifeUI(updateLifeCount);

            // 現在のライフ数を更新
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

            // i番目のアイコンを表示するかどうかを決める
            lifeIcons[i].SetActive(isActive);
        }
    }
}
