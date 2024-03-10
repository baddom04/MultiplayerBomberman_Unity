using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject blockWallPrefab;
    public GameObject groundPrefab;
    public Transform parentWall;
    public Transform parentGround;
    public PlayerController player1;
    public PlayerController player2;
    public Text endText;
    private bool gameOn;
    private bool hasEndBeenCalled = false;
    private GameObject[,] grid = new GameObject[9, 9];
    void Start()
    {
        gameOn = true;
        InitiateMap();
        endText.gameObject.SetActive(false);
    }
    void Update()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if ((i == player1.i && j == player1.j) || (i == player2.i && j == player2.j))
                {
                    grid[i, j].GetComponent<Renderer>().material.color = Color.gray;
                }
                else if (i % 2 != 1 || j % 2 != 1)
                {
                    grid[i, j].GetComponent<Renderer>().material.color = Color.green;
                }
            }
        }
        if(player1.CoordinateChanged()) Debug.Log("sldjfbl");

        if (gameOn && player1.isActiveAndEnabled && player2.isActiveAndEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space)) placeBomb(player1);
            if (Input.GetKeyDown(KeyCode.M)) placeBomb(player2);
        }
        else
        {
            gameOn = false;
        }

        if (!gameOn)
        {
            if (!hasEndBeenCalled)
            {
                StartCoroutine(End());
                hasEndBeenCalled = true;
            }
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
            if (Input.GetKey(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }
    private IEnumerator End()
    {
        yield return new WaitForSeconds(0.5f);
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
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
                    Instantiate(blockWallPrefab, new Vector3(j * 10 - 40, 2, (8 - i) * 10 - 40), Quaternion.identity, parentWall);
                }
                else
                {
                    grid[i, j] = Instantiate(groundPrefab, new Vector3(j * 10 - 40, 0, (8 - i) * 10 - 40), Quaternion.identity, parentGround);
                }
            }
        }
    }
    private void placeBomb(PlayerController player)
    {
        if (!player.isBomb) player.Bomb();
    }
}
