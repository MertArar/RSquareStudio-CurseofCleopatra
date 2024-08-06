using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoresCounter : MonoBehaviour
{
    public GameObject objectToCheck;
    public GameObject scoreLine;
    public GameObject redLine;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalCoinText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
    public Transform player;
    public GameObject goText;

    private List<int> highScores = new List<int>();
    private int currentScore;
    private int closeNum = 0;
    private int lineCr = 0;

    private float moveSpeed = 8f;
    private float speedIncreaseAmount = 0.2f;
    private float speedIncreaseInterval = 15f;
    private float secondForDistance;
    private int clicked = 0;

    void Start()
    {

        if (player == null)
        {
            Debug.LogError("Player reference is not set in the inspector.");
        }
        if (scoreText == null)
        {
            Debug.LogError("ScoreText reference is not set in the inspector.");
        }

        
        highScores.Add(PlayerPrefs.GetInt("HighScore1", 0));
        highScores.Add(PlayerPrefs.GetInt("HighScore2", 0));

        highScoreText.text = highScores[1].ToString();
        highScores[1] = 100; 
        
        UpdateScoreText();
        
    }

    void Update()
    {
        if (goText.activeInHierarchy == true && clicked == 0)
        {
            clicked++;
            StartCoroutine(IncreaseSpeed());
            StartCoroutine(UpdateScore());
        }

        int playerZ = Mathf.RoundToInt(player.position.z);

        if (lineCr == 0 && playerZ < highScores[1] - 50 && playerZ > highScores[1] - 100)
        {
            Vector3 vector3 = new Vector3(0, 1, highScores[1]);
            redLine.transform.position = vector3;
            lineCr++;
        }

        if (lineCr == 1 && playerZ >= highScores[1])
        {
            StartCoroutine(CanvasBestScore(playerZ));
            lineCr++;
        }

        if (objectToCheck.activeInHierarchy && closeNum == 0)
        {
            UpdateHighScores(currentScore);
            finalScoreText.text = currentScore.ToString();
            finalCoinText.text = coinText.text.ToString();
            closeNum++;
            Debug.Log("geberdi");
        }
    }

   
    void UpdateHighScores(int newScore)
    {
        if (newScore > highScores[0])
        {
            highScores[1] = highScores[0];
            highScores[0] = newScore;
            Debug.Log("Score kaydedildi");
        }
        else if (newScore > highScores[1])
        {
            highScores[1] = newScore;
        }

        PlayerPrefs.SetInt("HighScore1", highScores[0]);
        PlayerPrefs.SetInt("HighScore2", highScores[1]);
        PlayerPrefs.Save();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }
    IEnumerator UpdateScore()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(secondForDistance / 100);
            if (player != null)
            {
                secondForDistance = 100 / moveSpeed; // salise 
                currentScore += 1;
                UpdateScoreText();
            }
        }
    }

    IEnumerator IncreaseSpeed()
    {   
        yield return new WaitForSeconds(speedIncreaseInterval);
        moveSpeed += speedIncreaseAmount;
    }
    IEnumerator CanvasBestScore(int playerZ)
    {
        if (playerZ >= highScores[1]) { 
            Debug.Log("Çalıştı");
            
            scoreLine.SetActive(true);
            yield return new WaitForSeconds(2);
            scoreLine.SetActive(false);
        }
    }
}