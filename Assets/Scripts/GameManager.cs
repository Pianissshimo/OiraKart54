using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManagerをどこからでも使えるようにする
    public static GameManager Instance;

    // ゲームオーバー画面（CanvasのUI）
    public GameObject gameOverUI;

    public GameObject player;

    void Awake()
    {
        // シングルトン（1つだけ存在させる）
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン切り替えでも消えない
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ゲームオーバー処理
    public void GameOver()
    {
        // 子から切り離す
        Camera.main.transform.parent = null;

        // プレイヤーを消す
        if (player != null)
        {
            Destroy(player);
        }

        // UIを表示
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // 時間を止める（任意）
        Time.timeScale = 0f;
    }
}
