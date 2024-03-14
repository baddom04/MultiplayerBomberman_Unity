using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject boulderPrefab;
    private Transform parentEnvironment;
    private PlayerLogic player1;
    private PlayerLogic player2;
    [HideInInspector]
    public static bool gameOn = true;
    private bool hasEndBeenCalled = false;
    private float boulderSpawnY = -1;
    public float gridSize = 9;
    void Start()
    {
        player1 = GameObject.Find("Player1").GetComponent<PlayerLogic>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerLogic>();
        parentEnvironment = GameObject.FindWithTag("Environment").transform;
        InitiateMap();
    }
    void Update()
    {
        if (gameOn)
        {
            if (Input.GetKeyDown(KeyCode.M)) player1.PlaceBomb();
            if (Input.GetKeyDown(KeyCode.Space)) player2.PlaceBomb();
        }
        else
        {
            if (!hasEndBeenCalled)
            {
                hasEndBeenCalled = true;
                Camera.main.GetComponent<CameraScript>().TheEnd();
            }
            if (Input.GetKey(KeyCode.R))
                SceneManager.LoadScene("Main");
            if (Input.GetKey(KeyCode.Q))
                Application.Quit();
        }
    }
    private void InitiateMap()
    {
        PlayerPositions();
        OuterWalls();
        InnerWalls();
    }
    private void PlayerPositions(){
        player1.transform.position = new Vector3((gridSize - Mathf.Ceil(gridSize / 2)) * 10, player1.transform.position.y, player1.transform.position.z);
        Debug.Log((gridSize - Mathf.Ceil(gridSize / 2)) * -10);
        Debug.Log(player1.transform.position.x);
        player2.transform.position = new Vector3((gridSize - Mathf.Ceil(gridSize / 2)) * -10, player2.transform.position.y, player2.transform.position.z);
    }
    private void OuterWalls()
    {
        for (int i = -1; i < 2; i += 2)
        {
            for (int j = 0; j < (gridSize + 2); j++)
            {
                Instantiate(boulderPrefab, new Vector3((j - (gridSize - Mathf.Ceil(gridSize / 2)) - 1) * 10, boulderSpawnY, i * Mathf.Ceil(gridSize / 2) * 10), Quaternion.identity, parentEnvironment);
            }
        }
        for (int i = -1; i < 2; i += 2)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Instantiate(boulderPrefab, new Vector3(i * Mathf.Ceil(gridSize / 2) * 10, boulderSpawnY, (j - (gridSize - Mathf.Ceil(gridSize / 2))) * 10), Quaternion.identity, parentEnvironment);
            }
        }
    }
    private void InnerWalls()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (j % 2 == 1 && i % 2 == 1)
                {
                    Instantiate(boulderPrefab, new Vector3((j - (gridSize - Mathf.Ceil(gridSize / 2))) * 10, boulderSpawnY, (gridSize - 1 - i - (gridSize - Mathf.Ceil(gridSize / 2))) * 10), Quaternion.identity, parentEnvironment);
                }
            }
        }
    }
}
