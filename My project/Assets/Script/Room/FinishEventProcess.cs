using System;
using UnityEngine;

public class FinishEventProcess : MonoBehaviour
{
    public event Action OnFinishEvent;

    // イベントが終了したかの判定
    public bool isFinish = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        isFinish = true;

        // 終了イベントを実行
        FinishEventAction();
    }

    private void FinishEventAction()
    {
        Debug.Log("部屋から出ることができます");
        OnFinishEvent?.Invoke();
    }
}
