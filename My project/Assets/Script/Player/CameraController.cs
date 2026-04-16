using UnityEngine;

/// <summary>
/// プレイヤーに追従するカメラの制御クラス
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("カメラが追従するターゲット")]
    [SerializeField] private Transform targetTransform;

    [Header("ターゲットからの距離")]
    [SerializeField] private float targetDistance = 0.0f;

    private void Start()
    {
        // ターゲットからZ方向に離れる距離を設定
        Vector3 playerOffset =new Vector3(0, 0, -targetDistance);
    }
   
    private void Update()
    {
        // カメラをターゲットに追従させて、ターゲットからZ方向に離れる距離を保つ
        transform.position = targetTransform.position;
        transform.position += new Vector3(0, 0, -targetDistance);
    }
}
