using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    [System.Serializable]
    public class SaveData
    {
        public Vector3 playerPosition;
    }

    public static SaveManager Instance;

    private string savePath;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            savePath = Application.persistentDataPath + "/save.OiraKart54";
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {

    }

    public void SavePosition(Vector3 position)
    {
        SaveData data = new SaveData();

        data.playerPosition = position;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Saved to: " + savePath);
    }

    public Vector3 LoadPosition()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; // マウスカーソルを非表示＆画面中央に固定

            return data.playerPosition;
        }
        else //セーブデータが存在しないとき
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; // マウスカーソルを非表示＆画面中央に固定

            return Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
