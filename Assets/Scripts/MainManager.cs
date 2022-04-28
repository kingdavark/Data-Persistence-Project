using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    private Rigidbody Ball;

    private Text ScoreText;
    
    private bool m_Started = true;
    private int m_Points;
    
    private bool m_GameOver = false;

    public string playerName;

    public int highscoreMain = 0;
    public string highscorePlayer = "none";

    public static MainManager instance;

    private void Awake()
    {
        if(instance!= null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if(m_Points > highscoreMain)
        {
            SaveHighscore();    
        }
    }

    public void SetLevel()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint); 
            }
        }

        Ball = GameObject.Find("Ball").GetComponent<Rigidbody>();
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        m_Started = false;
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
    }

    public void SaveHighscore()
    {
        SaveData highscoreData = new SaveData();
        highscoreData.highscorePlayerName = playerName;
        highscoreData.highScore = m_Points;

        string json = JsonUtility.ToJson(highscoreData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData highscoreData = JsonUtility.FromJson<SaveData>(json);

            highscoreMain = highscoreData.highScore;
            highscorePlayer = highscoreData.highscorePlayerName;
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public string highscorePlayerName;
        public int highScore;
    }
        
}
