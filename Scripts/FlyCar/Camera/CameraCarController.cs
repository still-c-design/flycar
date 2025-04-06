using UnityEngine;

public class CameraCarController : MonoBehaviour
{
    [Header("setiings")]
    public Transform target; 
    public VehicleController playerController;
    public float baseDistance = 10.0f;
    public float baseHeight = 5.0f;
    public float maxDistance = 20.0f;
    public float maxHeight = 10.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    public float smoothSpeed = 0.125f;
    public LayerMask collisionLayers;

    private Vector3 currentVelocity;

    private void LateUpdate()
    {
        if (!target)
            return;

        // ターゲットのスピードを取得
        float speed = playerController.GetCurrentSpeed();
        float maxSpeed = playerController.GetMaxSpeed();

        // スピードに基づいて距離と高さを計算
        float distance = Mathf.Lerp(baseDistance, maxDistance, speed / maxSpeed);
        float height = Mathf.Lerp(baseHeight, maxHeight, speed / maxSpeed);

        // ターゲットの現在のY軸回転角度を取得
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Y軸回りの回転角度をスムーズに追従
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // 高さをスムーズに追従
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // 回転角度をQuaternionに変換
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // カメラの理想位置を計算
        Vector3 desiredPosition = target.position - currentRotation * Vector3.forward * distance + Vector3.up * height;
        RaycastHit hit;

        // 壁との衝突をチェック
        if (Physics.Linecast(target.position + Vector3.up * height, desiredPosition, out hit, collisionLayers))
        {
            desiredPosition = hit.point;
        }

        // スムーズにカメラの位置を更新
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);
        transform.position = smoothedPosition;

        // ターゲットを常に見つめるが、カメラの傾きを防ぐ
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(targetPosition);
    }
}
