using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseBtn;
    private void Show()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void QuitToMainMenu()
    {
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }
    public void Resume()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        pauseBtn.SetActive(true);
        isPaused = false;
    }
    public void Pause()
    {
        isPaused = true;
        pauseBtn.SetActive(false);
        Show();
    }
    public void UnPause()
    {
        isPaused = false;
    }
    public bool isPausedState()
    {
        return isPaused;
    }
}
