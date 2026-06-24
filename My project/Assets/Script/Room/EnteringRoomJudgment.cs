using System;
using UnityEngine;

public class EnteringRoomJudgment : MonoBehaviour
{
    public event Action OnPlayerEnterRoom;

    private bool isPlayerRoomm = false;

    // 部屋に１度でも入ったかの判定
    private bool isEntered = false;

    // この部屋をクリアしたか
    private bool isCleared = false;

    // この部屋のイベントを開始したか
    private bool hasStartedEvent = false;

    [SerializeField] private FinishEventProcess finishEventProcess;

    private void Start()
    {
        isPlayerRoomm = false;
        isEntered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEntered) return;
        Debug.Log("部屋に入りました");
        isPlayerRoomm = true;
        
        if (!collision.CompareTag("Player")) return;

        RoomManager.instance.SwitchRoomCamera(this);

        // クリア済みならイベントを開始しない
        if (isCleared)
        {
            return;
        }

        if (!hasStartedEvent)
        {
            hasStartedEvent = true;
        }

        OnPlayerEnterRoom?.Invoke();

        // イベント終了になったとき
        if (finishEventProcess.isFinish)
        {
            isCleared = true;

            // 退出できるイベント実行
            OnPlayerEnterRoom?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        Debug.Log("部屋を出ました。");
        isPlayerRoomm = false;
    }
}
