using UnityEngine;

public class ExitJudgment : MonoBehaviour
{
    // 当たり判定を切り替えるために呼び出すコライダー
    private new Collider2D collider;

    [Header("部屋の入り口のオブジェクトの開閉を切り替えるクラス")]
    [SerializeField] private EnteringRoomJudgment enteringRoomJudgment;

    [Header("部屋のイベントの終了を判定するクラス")]
    [SerializeField] private FinishEventProcess finishEventProcess;

    private void Start()
    {
        // 自身のコライダーを取得
        collider = GetComponent<Collider2D>();

        // 入り口のドアを閉じるイベントを登録
        enteringRoomJudgment.OnPlayerEnterRoom += SwitchingJudgementDoor;

        // 入り口のドアを開放するイベントを登録
        finishEventProcess.OnFinishEvent += SwitchingJudgementDoor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 当たったタグがプレイヤーではない場合何もしない
        if (!collision.CompareTag("Player")) return;
    }

    /// <summary>
    /// コライダのisTriggerを切り替えるメソッド
    /// イベントの実行時に使われる
    /// </summary>
    private void SwitchingJudgementDoor()
    {
        collider.isTrigger = !collider.isTrigger;

        //Debug.Log("ドアの判定を切り替えました");
    }
    
}
