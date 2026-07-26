using System;
using UnityEngine;

public class FinishEventProcess : MonoBehaviour
{
    // イベント終了時に実行するイベント
    public event Action OnFinishEvent;

    // イベントが終了したかの判定
    public bool isFinish = false;

    [SerializeField] NonCombatEventData eventData;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        isFinish = true;

        // 終了イベントを実行
        FinishEventAction();
    }

    private void FinishEventAction()
    {
        // イベントを終了するときに行う処理を行う
        Debug.Log("部屋から出ることができます");

        //EventUIManager.Instance.OpenPanel(eventData);

        OnFinishEvent?.Invoke();
    }
}
