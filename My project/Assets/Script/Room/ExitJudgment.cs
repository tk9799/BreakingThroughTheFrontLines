using UnityEngine;

public class ExitJudgment : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 当たったタグがプレイヤーではない場合何もしない
        if (!collision.CompareTag("Player")) return;

        RoomManager.instance.SpawnRoom(transform.position);
    }

    
}
