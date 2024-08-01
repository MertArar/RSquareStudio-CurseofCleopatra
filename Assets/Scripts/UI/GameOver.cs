using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CoinsCollected;
    [SerializeField] private TextMeshProUGUI DistanceRun;

    void OnEnable()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        CoinsCollected.text = PlayerPrefs.GetInt("CoinsCollected", 0).ToString();
        DistanceRun.text = PlayerPrefs.GetInt("DistanceRun", 0).ToString();
    }

    void Start()
    {
        PlayerPrefs.SetInt("CoinsCollected", 0);
        PlayerPrefs.Save();
    }

    public void Retry()
    {
        PlayerPrefs.SetInt("CoinsCollected", 0);
        PlayerPrefs.Save();

        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}