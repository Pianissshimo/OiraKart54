using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivityX = 1000f;
    public float mouseSensitivityY = 500f;
    public Transform playerBody; // カメラの親（プレイヤー本体）

    float xRotation = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // マウスカーソルを非表示＆画面中央に固定
        Cursor.visible = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        // 上下の視点（X軸回転）を制限（首が回りすぎないように）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // カメラの上下回転
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // プレイヤーの左右回転
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
