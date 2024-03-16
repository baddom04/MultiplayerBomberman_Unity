using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject boulderPrefab;
    [SerializeField] private GameObject cratePrefab;
    public GameObject[,] level = new GameObject[gridSize, gridSize];
    [SerializeField] private GameObject[] pickups;
    private Transform parentEnvironment;
    private PlayerLogic player1;
    private PlayerLogic player2;
    public static bool gameOn = true;
    [SerializeField] private bool hasEndBeenCalled = false;
    private float boulderSpawnY = -1;
    public static int gridSize = 11;
    private int maxGridIndex = gridSize - 1;
    [SerializeField] private float crateChance = 0.2f;
    [SerializeField] private float pickupChance = 0.2f;

    void Start()
    {
        player1 = GameObject.Find("Player1").GetComponent<PlayerLogic>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerLogic>();
        parentEnvironment = GameObject.FindWithTag("Environment").transform;
        InitiateMap();
    }
    void Update()
    {
        if (gameOn && !PauseMenu.isPausedState())
        {
            if (Input.GetKeyDown(KeyCode.M)) player1.PlaceBomb();
            if (Input.GetKeyDown(KeyCode.Space)) player2.PlaceBomb();
            if (Input.GetKeyDown(KeyCode.Escape)) PauseMenu.Pause();
        }
        else if (!gameOn && !hasEndBeenCalled)
        {
            hasEndBeenCalled = true;
            GameObject.Find("PauseBtn").SetActive(false);
            Camera.main.GetComponent<CameraScript>().TheEnd();
        }
    }
    private void InitiateMap()
    {
        InnerWallsAndCrates();
        PlayerPositions();
        OuterWalls();
    }
    private void PlayerPositions()
    {
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
                    level[i, j] = Instantiate(boulderPrefab, new Vector3(x, boulderSpawnY, y), Quaternion.identity, parentEnvironment);
                }
                else if (CalculateChace(crateChance) && NotInPlayer(i, j))
                {
                    level[i, j] = Instantiate(cratePrefab, new Vector3(x, 0, y), Quaternion.identity, parentEnvironment);
                    if(CalculateChace(pickupChance))
                        Instantiate(pickups[Random.Range(0, pickups.Length)], new Vector3(x, 3.5f, y), Quaternion.identity, parentEnvironment);
                }
            }
        }
    }
    private bool CalculateChace(float chance)
    {
        return UnityEngine.Random.Range(0, (int)(1 / chance)) == 0;
    }
    private bool NotInPlayer(int i, int j)
    {
        return (j != 0 || i != gridSize / 2 ) && (j != maxGridIndex || i != gridSize / 2);
    }
}
