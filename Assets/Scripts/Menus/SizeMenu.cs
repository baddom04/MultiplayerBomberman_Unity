using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SizeMenu : MonoBehaviour
{
    [SerializeField] private RectTransform[] uiElements;
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private float distance = 80;
    [SerializeField] private int size;
    public void Show()
    {
        uiElements = new RectTransform[transform.childCount];
        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i] = transform.GetChild(i).GetComponent<RectTransform>();
            StartCoroutine(Animation(i));   
        }
    }
    private IEnumerator Animation(int i)
    {
        Vector2 finalPos = uiElements[i].anchoredPosition;
        uiElements[i].anchoredPosition += Vector2.down * distance;
        Vector2 beginPos = uiElements[i].anchoredPosition;
        uiElements[i].gameObject.SetActive(true);
        float timeElapsed = 0;
        while (timeElapsed < animationDuration)
        {
            float t = timeElapsed / animationDuration;
            uiElements[i].anchoredPosition = Vector2.Lerp(beginPos, finalPos, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
    public void NewGame()
    {
        GameController.gameOn = true;
        GameController.gridSize = size;
        SceneManager.LoadScene("Game");
    }
    public void SetSize(int size){
        this.size = size;
    }
}
