using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameSceneに遷移するためのクラス
/// </summary>
public class GameSceneMove : MonoBehaviour
{
    /// <summary>
    /// GameSceneに遷移するメソッド
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
