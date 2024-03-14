using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject boulderPrefab;
    public GameObject cratePrefab;
    private Transform parentEnvironment;
    private PlayerLogic player1;
    private PlayerLogic player2;
    [HideInInspector]
    public bool gameOn = true;
    private bool hasEndBeenCalled = false;
    private float boulderSpawnY = -1;
    public int gridSize = 9;
    private float crateChance = 0.15f;
    private float formula;
    private int maxGridIndex;
    void Start()
    {
        maxGridIndex = gridSize - 1;
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
        InnerWallsAndCrates();
    }
    private void PlayerPositions(){
        int x = gridSize / 2 * 10;
        player1.transform.position = new Vector3(x, player1.transform.position.y, player1.transform.position.z);
        player2.transform.position = new Vector3(-x, player2.transform.position.y, player2.transform.position.z);
    }
    private void OuterWalls()
    {
        for (int i = -1; i < 2; i += 2)
        {
            for (int j = 0; j < (gridSize + 2); j++)
            {
                int x = (j - gridSize / 2 - 1) * 10;
                int y = i * (gridSize / 2 + 1) * 10;
                Instantiate(boulderPrefab, new Vector3(x, boulderSpawnY, y), Quaternion.identity, parentEnvironment);
            }
        }
        for (int i = -1; i < 2; i += 2)
        {
            for (int j = 0; j < gridSize; j++)
            {
                int x = i * (gridSize / 2 + 1) * 10;
                int y = (j - gridSize / 2) * 10;
                Instantiate(boulderPrefab, new Vector3(x, boulderSpawnY, y), Quaternion.identity, parentEnvironment);
            }
        }
    }
    private void InnerWallsAndCrates()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                int x = (j - gridSize / 2) * 10;
                int y = (maxGridIndex - i - gridSize / 2) * 10;
                if (j % 2 == 1 && i % 2 == 1)
                {
                    Instantiate(boulderPrefab, new Vector3(x, boulderSpawnY, y), Quaternion.identity, parentEnvironment);
                }
                else if(CalculateChace() && NotInPlayer(i, j)){
                    Instantiate(cratePrefab, new Vector3(x, 0, y), Quaternion.identity, parentEnvironment);
                }
            }
        }
    }
    private bool CalculateChace(){
        return Random.Range(0, (int)(1 / crateChance)) == 0;
    }
    private bool NotInPlayer(int i, int j){
        return j != 0 && j != maxGridIndex && i != gridSize / 2;
    }
}
