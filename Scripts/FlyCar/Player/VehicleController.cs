using UnityEngine;
using UnityEngine.UI;

public class VehicleController : MonoBehaviour
{
    [Header("wheel Objects")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;
    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform rearLeftTransform;
    public Transform rearRightTransform;

    [Header("settings")]
    public float maxMotorTorque = 1500f;
    public float maxSteeringAngle = 30f;
    public float maxBrakeTorque = 3000f;
    public float steeringSensitivity = 0.5f;
    public float maxSpeed = 150f;
    public Text speedText;
    public float currentSpeed;

    [Header("flying settings")]
    public float liftForce = 20000f;
    public float moveForce = 10000f;
    public float rotationSpeed = 50f;
    public float tiltSpeed = 50f;
    public float maxTiltAngle = 15f;
    public float gravity = -9.8f;
    public float brakeForce = 0.985f;
    private Vector3 moveDirection = Vector3.zero;

    private Rigidbody _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = new Vector3(0, -0.5f, 0);

        SetWheelSuspension(frontLeftWheel);
        SetWheelSuspension(frontRightWheel);
        SetWheelSuspension(rearLeftWheel);
        SetWheelSuspension(rearRightWheel);
        SetWheelFriction(frontLeftWheel);
        SetWheelFriction(frontRightWheel);
        SetWheelFriction(rearLeftWheel);
        SetWheelFriction(rearRightWheel);
    }

    void Update()
    {
        currentSpeed = _rb.velocity.magnitude;

        HandleHelicopterInput();
        ClampTilt();

        moveDirection.y += gravity * Time.deltaTime;
    }

    void FixedUpdate()
    {
        HandleCarMovement();
        HandleHelicopterMovement();
    }

    // 車の動きの処理
    void HandleCarMovement()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float brake = Input.GetKey(KeyCode.Space) ? maxBrakeTorque : 0f;

        float currentSteeringAngle = Mathf.Lerp(frontLeftWheel.steerAngle, maxSteeringAngle * Input.GetAxis("Horizontal") * steeringSensitivity, Time.deltaTime * 5f);
        frontLeftWheel.steerAngle = currentSteeringAngle;
        frontRightWheel.steerAngle = currentSteeringAngle;

        rearLeftWheel.motorTorque = motor;
        rearRightWheel.motorTorque = motor;

        ApplyBrakes(brake);

        UpdateWheelPos(frontLeftWheel, frontLeftTransform);
        UpdateWheelPos(frontRightWheel, frontRightTransform);
        UpdateWheelPos(rearLeftWheel, rearLeftTransform);
        UpdateWheelPos(rearRightWheel, rearRightTransform);

        UpdateSpeedometer();
    }

    // ブレーキの設定
    void ApplyBrakes(float brakeTorque)
    {
        frontLeftWheel.brakeTorque = brakeTorque;
        frontRightWheel.brakeTorque = brakeTorque;
        rearLeftWheel.brakeTorque = brakeTorque;
        rearRightWheel.brakeTorque = brakeTorque;
    }

    // サスペンションの設定
    void SetWheelSuspension(WheelCollider wheel)
    {
        JointSpring spring = wheel.suspensionSpring;
        spring.spring = 35000;
        spring.damper = 4500;
        spring.targetPosition = 0.5f;
        wheel.suspensionSpring = spring;
    }

    // 摩擦の設定
    void SetWheelFriction(WheelCollider wheel)
    {
        WheelFrictionCurve forwardFriction = wheel.forwardFriction;
        forwardFriction.extremumSlip = 0.4f;
        forwardFriction.extremumValue = 1f;
        forwardFriction.asymptoteSlip = 0.8f;
        forwardFriction.asymptoteValue = 0.5f;
        forwardFriction.stiffness = 1.5f;
        wheel.forwardFriction = forwardFriction;

        WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;
        sidewaysFriction.extremumSlip = 0.2f;
        sidewaysFriction.extremumValue = 1f;
        sidewaysFriction.asymptoteSlip = 0.5f;
        sidewaysFriction.asymptoteValue = 0.75f;
        sidewaysFriction.stiffness = 2f;
        wheel.sidewaysFriction = sidewaysFriction;
    }

    // ホイールの更新
    void UpdateWheelPos(WheelCollider col, Transform trans)
    {
        Vector3 pos;
        Quaternion quat;
        col.GetWorldPose(out pos, out quat);
        trans.position = pos;
        trans.rotation = quat;
    }

    // スピードメーターの更新
    void UpdateSpeedometer()
    {
        float speed = _rb.velocity.magnitude * 3.6f;
        speedText.text = speed.ToString("0") + " km/h";
    }

    // 移動系
    void HandleHelicopterInput()
    {
        //　右に旋回
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        //　左に旋回
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        //　前進
        if (Input.GetKey(KeyCode.W))
        {
            _rb.AddRelativeForce(Vector3.forward * moveForce);
        }
        //　後退
        if (Input.GetKey(KeyCode.S))
        {
            _rb.AddRelativeForce(-Vector3.forward * moveForce);
        }
        //　ブレーキ
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyBrakeForce();
        }
    }

    // 上昇系
    void HandleHelicopterMovement()
    {
        //　上昇
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rb.AddForce(Vector3.up * liftForce);
        }
        //　下降
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rb.AddForce(Vector3.down * liftForce * 0.5f);
        }
        
    }

    // ブレーキ力の適用
    void ApplyBrakeForce()
    {
        _rb.velocity *= brakeForce;
    }

    // 傾斜角度の制限　ひっくり返ると動けなくなるためそれを防ぐ
    void ClampTilt()
    {
        Vector3 localEulerAngles = transform.localEulerAngles;
        localEulerAngles.x = NormalizeAngle(localEulerAngles.x);
        localEulerAngles.z = NormalizeAngle(localEulerAngles.z);
        localEulerAngles.x = Mathf.Clamp(localEulerAngles.x, -maxTiltAngle, maxTiltAngle);
        localEulerAngles.z = Mathf.Clamp(localEulerAngles.z, -maxTiltAngle, maxTiltAngle);
        transform.localEulerAngles = localEulerAngles;
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        if (angle < -180f) angle += 360f;
        return angle;
    }


    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
}
