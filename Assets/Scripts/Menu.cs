using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private RectTransform[] uiElements;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float distance = 80;
    [SerializeField] private bool isMainMenu;
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
        Application.Quit();
    }
    public void NewGame(){
        GameController.gameOn = true;
        SceneManager.LoadScene("Game");
    }
}
