using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// StartSceneに遷移するためのクラス
/// </summary>
public class GameStartSceneMove : MonoBehaviour
{
    public void MoveGameStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
