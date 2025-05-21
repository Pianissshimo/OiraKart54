using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody;        // プレイヤーの体（親オブジェクト）
    public Transform cameraRig;         // CameraRig（空オブジェクト）、playerBodyの子
    public Transform cameraTransform;   // Main Camera（cameraRigの子）

    public Vector3 firstPersonOffset = new Vector3(0f, 1.6f, 0f);
    public Vector3 thirdPersonOffset = new Vector3(0f, 3f, -6f);

    public float mouseSensitivityX = 1000f;
    public float mouseSensitivityY = 500f;

    private float xRotation = 0f;
    private bool isFirstPerson = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // マウスを画面中央に固定
        Cursor.visible = false;

        // 初期は一人称視点にセット
        SwitchToFirstPerson();
    }

    void Update()
    {
        // マウス入力
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        // プレイヤーの左右回転（Y軸）
        playerBody.Rotate(Vector3.up * mouseX);

        // 上下視点の回転量を制限
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (isFirstPerson)
        {
            // 一人称視点：cameraRigはplayerBodyの子なのでローカル回転で上下視点を操作
            cameraRig.localPosition = firstPersonOffset;
            cameraRig.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        else
        {
            // 三人称視点：cameraRigの位置をplayerBodyの後方に設定
            cameraRig.localPosition = thirdPersonOffset;

            // カメラ（cameraTransform）をplayerBodyの頭の方向に向ける
            Vector3 lookTarget = playerBody.position + Vector3.up * 1.6f;
            cameraTransform.LookAt(lookTarget);
            // cameraRigはplayerBodyの子のまま回転はリセットしておく（回転はcameraTransformに任せる）
            cameraRig.localRotation = Quaternion.identity;
        }

        // 視点切り替え
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson;
            if (isFirstPerson) SwitchToFirstPerson();
            else SwitchToThirdPerson();
        }
    }

    void SwitchToFirstPerson()
    {
        // 一人称視点の初期設定
        cameraRig.localPosition = firstPersonOffset;
        cameraRig.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void SwitchToThirdPerson()
    {
        // 三人称視点の初期設定
        cameraRig.localPosition = thirdPersonOffset;
        cameraRig.localRotation = Quaternion.identity;
    }
}
