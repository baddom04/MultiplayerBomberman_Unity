using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private RectTransform[] uiElements;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float distance = 80;
    [SerializeField] private bool isMainMenu;
    [SerializeField] GameObject nextMenu;
    private void Start() {
        if(isMainMenu) Show();
    }
    public void Show() {
        uiElements = new RectTransform[transform.childCount];
        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }
        StartCoroutine(Animation());
    }
    private IEnumerator Animation(){
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector2 finalPos = uiElements[i].anchoredPosition;
            uiElements[i].anchoredPosition += Vector2.down * distance;
            Vector2 beginPos = uiElements[i].anchoredPosition;
            uiElements[i].gameObject.SetActive(true);
            float timeElapsed = 0;
            while(timeElapsed < animationDuration){ 
                float t = timeElapsed / animationDuration;
                uiElements[i].anchoredPosition = Vector2.Lerp(beginPos, finalPos, t);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
    public void Quit(){
        if(isMainMenu){
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
            #else
                Application.Quit();
            #endif
        } 
        else SceneManager.LoadScene("MainMenu");
    }
    public void NextMenu(){
        foreach(RectTransform rt in uiElements){
            rt.gameObject.SetActive(false);
        }
        nextMenu.GetComponent<SizeMenu>().Show();
    }
    public void NewGame(){
        GameController.gameOn = true;
        SceneManager.LoadScene("Game");
    }
}
