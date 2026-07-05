using UnityEngine;

public class RoomCameraController : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("RoomCameraController Start");

        // ゲーム開始時SwitchCameraTransformメソッドをイベント登録
        RoomManager.instance.OnSwitchRoom += SwitchCameraTransform;
    }

    private void OnDestroy()
    {
        if(RoomManager.instance != null)
        {
            RoomManager.instance.OnSwitchRoom -= SwitchCameraTransform;
        }
    }

    private void SwitchCameraTransform(EnteringRoomJudgment roomFloor)
    {
        Debug.Log("カメラを移動します");

        transform.position = new Vector3(roomFloor.transform.position.x,
            roomFloor.transform.position.y,
            transform.position.z);
    }
}
