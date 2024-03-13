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
    private float gridSize = 9;
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
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (j % 2 == 1 && i % 2 == 1)
                {
                    Instantiate(boulderPrefab, new Vector3(j * 10 - 40, boulderSpawnY, (8 - i) * 10 - 40), Quaternion.identity, parentEnvironment);
                }
            }
        }
    }
}
