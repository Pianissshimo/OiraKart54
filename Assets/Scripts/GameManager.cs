using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GameManager���ǂ�����ł��g����悤�ɂ���
    public static GameManager Instance;

    // �Q�[���I�[�o�[��ʁiCanvas��UI�j
    public GameObject gameOverUI;

    public GameObject currentPlayer;
    public GameObject playerPrefab;

    public Vector3 respawnPosition = new Vector3(0f, 5f, 0f);

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
        SpawnPlayer();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // �}�E�X�J�[�\�����\������ʒ����ɌŒ�

        SaveManager.Instance.SavePosition(currentPlayer.transform.position);
    }

    // �Q�[���I�[�o�[����
    public void GameOver()
    {
        if (currentPlayer != null)
        {
            Camera.main.transform.parent = null;

            Destroy(currentPlayer);
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
        SpawnPlayer();

        gameOverUI.SetActive(false);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        currentPlayer.transform.position = SaveManager.Instance.LoadPosition(); //player���ŏ��̈ʒu�ɖ߂�
        
        Time.timeScale = 1f;
    }

    void SpawnPlayer()
    {
        currentPlayer = Instantiate(playerPrefab, respawnPosition, Quaternion.identity);

        Camera.main.transform.SetParent(currentPlayer.transform, false);
        Camera.main.transform.localPosition = Chiba_Camera.thirdPersonOffset;

        Camera.main.GetComponent<Chiba_Camera>().playerBody = currentPlayer.transform;
    }
}
