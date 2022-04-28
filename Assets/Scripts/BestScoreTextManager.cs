using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreTextManager : MonoBehaviour
{
    public Text bestScoreText;

    private void Awake()
    {
        MainManager.instance.LoadHighscore();
        MainManager.instance.SetLevel();
    }

    private void Start()
    {
        bestScoreText.text = $"Best Score: {MainManager.instance.highscorePlayer} : {MainManager.instance.highscoreMain}";
    }
}
