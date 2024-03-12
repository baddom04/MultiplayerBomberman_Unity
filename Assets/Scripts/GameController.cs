using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject blockWallPrefab;
    public GameObject groundPrefab;
    private Transform parentEnvironment;
    public PlayerLogic player1;
    public PlayerLogic player2;
    public Text endText;
    private bool gameOn = true;
    private bool hasEndBeenCalled = false;
    private GameObject[,] grid = new GameObject[9, 9];
    void Start()
    {
        parentEnvironment = GameObject.FindWithTag("Environment").transform;
        InitiateMap();
        endText.gameObject.SetActive(false);
    }
    void Update()
    {
        if (gameOn && player1.isActiveAndEnabled && player2.isActiveAndEnabled)
        {
            if (Input.GetKeyDown(KeyCode.M)) placeBomb(player1);
            if (Input.GetKeyDown(KeyCode.Space)) placeBomb(player2);
        }
        else
        {
            gameOn = false;
        }

        if (!gameOn)
        {
            if (!hasEndBeenCalled)
            {
                hasEndBeenCalled = true;
                StartCoroutine(End());
            }
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("MainScene");
            }
            if (Input.GetKey(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }
    public void ChangeTileColors()
    {
        grid[player1.last_i, player1.last_j].GetComponent<Renderer>().material.color = Color.green;
        grid[player2.last_i, player2.last_j].GetComponent<Renderer>().material.color = Color.green;
        grid[player1.i, player1.j].GetComponent<Renderer>().material.color = new Color(0.0f, 0.7f, 0.0f);
        grid[player2.i, player2.j].GetComponent<Renderer>().material.color = new Color(0.0f, 0.7f, 0.0f);
    }
    private IEnumerator End()
    {
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < 30; i++){
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(0, 0, 0), 0.01f);
        }
        endText.gameObject.SetActive(true);
        endText.text = "Congratulations! Press R to restart! Press Q to quit!";
    }
    private void InitiateMap()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (j % 2 == 1 && i % 2 == 1)
                {
                    Instantiate(blockWallPrefab, new Vector3(j * 10 - 40, blockWallPrefab.transform.localScale.y / 2, (8 - i) * 10 - 40), Quaternion.identity, parentEnvironment);
                }
                else
                {
                    grid[i, j] = Instantiate(groundPrefab, new Vector3(j * 10 - 40, 0, (8 - i) * 10 - 40), Quaternion.identity, parentEnvironment);
                }
            }
        }
    }
    private void placeBomb(PlayerLogic player)
    {
        if (!player.isBomb) player.Bomb();
    }
}
