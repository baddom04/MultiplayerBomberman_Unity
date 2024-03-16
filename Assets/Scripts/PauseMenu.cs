using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool isPaused = false;
    private bool hasPauseBeenCalled = false;
    [SerializeField] private GameObject pauseBtn;
    void Update(){
        if(isPaused && !hasPauseBeenCalled){
            pauseBtn.SetActive(false);
            hasPauseBeenCalled = true;
            Show();
        }
    }
    public void Show() {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public void QuitToMainMenu(){
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }
    public void Resume(){
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        pauseBtn.SetActive(true);
        isPaused = false;
        hasPauseBeenCalled = false;
    }
    public static void Pause(){
        isPaused = true;
    }
    public static bool isPausedState(){
        return isPaused;
    }
}
