using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBtns : MonoBehaviour
{
    public void Quit(){
        Application.Quit();
    }
    public void NewGame(){
        SceneManager.LoadScene("Game");
    }
}
