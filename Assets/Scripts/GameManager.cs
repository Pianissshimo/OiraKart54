using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GameManager���ǂ�����ł��g����悤�ɂ���
    public static GameManager Instance;

    // �Q�[���I�[�o�[��ʁiCanvas��UI�j
    public GameObject gameOverUI;

    public GameObject player;

    public Rigidbody rb;

    void Awake()
    {
        // �V���O���g���i1�������݂�����j
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���؂�ւ��ł������Ȃ�
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // �}�E�X�J�[�\�����\������ʒ����ɌŒ�

        SaveManager.Instance.SavePosition(player.transform.position);
    }

    // �Q�[���I�[�o�[����
    public void GameOver()
    {
        if (player != null)
        {
            Camera.main.transform.parent = null;

            player.SetActive(false);
        }

        // UI��\��
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("GameOver����");

        // ���Ԃ��~�߂�i�C�Ӂj
        Time.timeScale = 0f;
    }
    
    public void RetryGame()
    {
        player.SetActive(true);
        Camera.main.transform.SetParent(player.transform, false);
        Camera.main.transform.position = Chiba_Camera.retryCameraPosition;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        transform.rotation = Quaternion.identity; // ��]�����Z�b�g

        gameOverUI.SetActive(false);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        player.transform.position = SaveManager.Instance.LoadPosition(); //player���ŏ��̈ʒu�ɖ߂�
        
        Time.timeScale = 1f;
    }

}
