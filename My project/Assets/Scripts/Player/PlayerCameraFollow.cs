using UnityEngine;

/// <summary>
/// プレイヤーを追従するカメラクラス
/// </summary>
public class PlayerCameraFollow : MonoBehaviour
{
    [Header("追従対象")]
    [SerializeField] private Transform target = null;

    [Header("ズーム設定")]
    [SerializeField] private float cameraSize = 8f;

    [Header("カメラ位置補正")]
    [SerializeField] private Vector3 offset = Vector3.zero;

    //Cameraコンポーネント
    private Camera cameraComponent = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        //Cameraコンポーネントを取得
        cameraComponent = GetComponent<Camera>();

        //カメラの表示範囲を設定
        cameraComponent.orthographicSize = cameraSize;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void LateUpdate()
    {
        //オフセットを考慮した追従先の位置を取得
        Vector3 targetPosition = target.position + offset;

        //カメラのZ座標を維持
        targetPosition.z = transform.position.z;

        //カメラを移動
        transform.position = targetPosition;
    }
}