using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float distance = 80;
    public static bool isPaused = false;
    private bool hasPauseBeenCalled = false;
    void Update(){
        if(isPaused && !hasPauseBeenCalled){
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
        isPaused = false;
        hasPauseBeenCalled = false;
    }
}
