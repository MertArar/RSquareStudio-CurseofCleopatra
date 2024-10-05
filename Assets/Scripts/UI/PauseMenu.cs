using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] private GameObject goText;


    void Update()
    {
        if (goText.activeSelf == true)
        {
            PlayerMovement.canMove = true;
            Debug.Log("calisti");
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            EditorApplication.playModeStateChanged += StateChanged;
        }
        else
        {
            EditorApplication.isPlaying = true;
        }
#else
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
    }

#if UNITY_EDITOR
    private static void StateChanged(PlayModeStateChange change)
    {
        if (change == PlayModeStateChange.EnteredEditMode)
        {
            EditorApplication.isPlaying = true;
            EditorApplication.playModeStateChanged -= StateChanged;
        }
    }
#endif

}