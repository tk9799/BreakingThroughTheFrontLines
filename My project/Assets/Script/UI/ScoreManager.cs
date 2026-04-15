using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score = 0;

    [SerializeField] private TMP_Text scoreText;

    // オブジェクト有効になった時に自動で呼ばれる関数
    private void OnEnable()
    {
        EnemyLife.onAddScore += AddScore;
    }

    private void OnDisable()
    {
        EnemyLife.onAddScore -= AddScore;
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    /// <summary>
    /// スコアを加算する内部処理をしているメソッド
    /// </summary>
    private void AddScore(int value)
    {
        score += value;
        UpdateScoreUI();
        Debug.Log("Score: " + score);
    }

    /// <summary>
    /// スコアをUIに反映させるメソッド
    /// </summary>
    private void UpdateScoreUI()
    {
        scoreText.text = "Score : " + score;

        if (score > 5000)
        {
            SceneManager.LoadScene("ClearScene");
        }
    }
}
