using UnityEngine;

public class Chiba_Camera : MonoBehaviour
{
    private float mouseX = 0f;
    private float mouseY = 0f;
    public float mouseSensitivityX = 1000f;
    public float mouseSensitivityY = 500f;
    public Transform playerBody; // カメラの親（プレイヤー本体）

    private bool isFirstPerson = true;// 現在の視点状態（初期は一人称）

    // カメラの位置（ローカル座標）
    private Vector3 firstPersonOffset = new Vector3(0f, 0f, 0f);  // 一人称視点
    public Vector3 thirdPersonOffset; // 三人称視点（後ろ上）

    private float camera_y;

    public static Vector3 retryCameraPosition;

    private float xRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera_y = thirdPersonOffset.y;

        retryCameraPosition = thirdPersonOffset;
        /*
        transform.localPosition = new Vector3(0f, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        */
        SwitchToFirstPerson();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isFirstPerson)
            {
                SwitchToThirdPerson();
            }
            else
            {
                SwitchToFirstPerson();
            }

            isFirstPerson = !isFirstPerson;
        }

        mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        // 上下の視点（X軸回転）を制限（首が回りすぎないように）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // カメラの上下回転
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if(playerBody != null)
        {
            // プレイヤーの左右回転
            playerBody.Rotate(Vector3.up * mouseX);
        }

        KeepCameraPosition();
    }

    void SwitchToFirstPerson()
    {
        transform.localPosition = firstPersonOffset;
        transform.localRotation = Quaternion.identity; // 必要なら回転もリセット
    }

    void SwitchToThirdPerson()
    {
        transform.localPosition = thirdPersonOffset;
        transform.localRotation = Quaternion.Euler(19f, 0f, 0f); // 必要なら回転も調整
    }

    void KeepCameraPosition()
    {
        if (isFirstPerson)
        {
            return;
        }
        // 3人称視点でカメラのY座標を常に一定に保つ
        Vector3 pos = this.transform.position;
        pos.y = camera_y;
        this.transform.position = pos;
    }
}
