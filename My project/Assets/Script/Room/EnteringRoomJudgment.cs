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

        // カメラを自分の位置に切り替えるイベントを実行する
        RoomManager.instance.SwitchRoomCamera(this);

        // クリア済みならイベントを開始しない
        if (isCleared)
        {
            return;
        }

        // プレイヤーが部屋に入ったときhasStartedEvent = true;にする
        if (!hasStartedEvent)
        {
            hasStartedEvent = true;
        }

        // プレイヤーが部屋に入ったときに実行するイベントを実行する
        OnPlayerEnterRoom?.Invoke();

        // イベント終了になったとき
        if (finishEventProcess.isFinish)
        {
            isCleared = true;

            // 退出できるイベント実行
            OnPlayerEnterRoom?.Invoke();
        }
    }

    /// <summary>
    /// プレイヤーが部屋から出た時の判定
    /// イベントを繰り返ししないためにboolを切り替える
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        Debug.Log("部屋を出ました。");
        isPlayerRoomm = false;
    }
}
