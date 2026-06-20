using System;
using UnityEngine;

public class EnteringRoomJudgment : MonoBehaviour
{
    public event Action OnPlayerEnterRoom;

    private bool isEntered = false;

    [SerializeField] private FinishEventProcess finishEventProcess;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEntered) return;

        if (!collision.CompareTag("Player")) return;

        isEntered = true;

        if (!finishEventProcess.isFinish)
        {
            OnPlayerEnterRoom?.Invoke();
        }
    }
}
