using UnityEngine;

public class RoomCameraController : MonoBehaviour
{
    private EnteringRoomJudgment enteringRoomJudgment;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Debug.Log("RoomCameraController Start");

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
        Debug.Log("ÉJÉÅÉâÇà⁄ìÆÇµÇ‹Ç∑");

        transform.position = new Vector3(roomFloor.transform.position.x,
            roomFloor.transform.position.y,
            transform.position.z);
    }
}
