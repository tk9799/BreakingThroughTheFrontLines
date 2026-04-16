using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [Header("スコアの値")]
    [SerializeField] private int score = 0;

    [Header("スコア表示用テキスト")]
    [SerializeField] private TMP_Text scoreText;

    // オブジェクト有効になった時に自動で呼ばれる関数
    private void OnEnable()
    {
        EnemyLife.onAddScore += AddScore;
    }

    // オブジェクト無効になった時に自動で呼ばれる関数
    private void OnDisable()
    {
        EnemyLife.onAddScore -= AddScore;
    }

    private void Start()
    {
        // スコアを初期化してUIに反映させる
        UpdateScoreUI();
    }

    /// <summary>
    /// スコアを加算する内部処理をしているメソッド
    /// </summary>
    private void AddScore(int value)
    {
        // 呼び出された時の引数分だけスコアを加算する
        score += value;

        // スコアをUIに反映させる
        UpdateScoreUI();
        Debug.Log("Score: " + score);
    }

    /// <summary>
    /// スコアをUIに反映させるメソッド
    /// </summary>
    private void UpdateScoreUI()
    {
        // スコアをテキストに反映させる
        scoreText.text = "Score : " + score;

        // スコアが5000点を超えたらクリアシーンに遷移する
        if (score > 5000)
        {
            SceneManager.LoadScene("ClearScene");
        }
    }
}
