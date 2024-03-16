using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    private float bombWaitingTime = 3;
    private int i;
    private int j;
    private Rigidbody rb;
    private List<BombScript> bombs = new List<BombScript>();
    [SerializeField] private int maxBombCount = 1;
    [SerializeField] private int currentBombs = 0;
    [SerializeField] private bool isOnBomb = false;
    [SerializeField] private int bombRadius = 1;
    private int last_i;
    private int last_j;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        last_j = (int)((transform.position.x + 5 + GameController.gridSize / 2 * 10) / 10);
        last_i = GameController.gridSize - 1 - (int)((transform.position.z + 5 + GameController.gridSize / 2 * 10) / 10);
    }
    void Update()
    {
        i = GameController.gridSize - 1 - (int)((transform.position.z + 5 + GameController.gridSize / 2 * 10) / 10);
        j = (int)((transform.position.x + 5 + GameController.gridSize / 2 * 10) / 10);
        if (CoordinateChanged())
        {
            Debug.Log(i + " " + j);
            if (PlayerGoesWhereItShouldntBe())
            {
                int x = (last_j - GameController.gridSize / 2) * 10;
                int z = (GameController.gridSize - 1 - last_i - GameController.gridSize / 2) * 10;
                transform.position = new Vector3(x, 0, z);
            }
            else
            {
                last_i = i;
                last_j = j;
                if (isOnBomb){
                    bombs[bombs.Count - 1].GetComponent<Collider>().isTrigger = false;
                    isOnBomb = false;
                }
            }
        }
    }
    private bool PlayerGoesWhereItShouldntBe()
    {
        GameObject[,] level = GameObject.Find("GameController").GetComponent<GameController>().level;
        return level[i, j] != null;
    }

    public void PlaceBomb()
    {
        if (currentBombs < maxBombCount)
        {
            int x = (j - GameController.gridSize / 2) * 10;
            int z = (GameController.gridSize - 1 - i - GameController.gridSize / 2) * 10;
            BombScript b = Instantiate(bombPrefab, new Vector3(x, 2, z), Quaternion.identity).GetComponent<BombScript>();
            b.i = i;
            b.j = j;
            b.radius = bombRadius;
            bombs.Add(b);
            currentBombs++;
            isOnBomb = true;
            StartCoroutine(BombCountDown(b));
        }
    }
    public bool CoordinateChanged()
    {
        return last_i != i || last_j != j;
    }
    private IEnumerator BombCountDown(BombScript bs)
    {
        yield return new WaitForSeconds(bombWaitingTime);
        currentBombs--;
        bombs.RemoveAt(0);
        bs.Detonation();
    }

    //Power-up functions
    public void IncreaseRadius(){
        bombRadius++;
    }
    public void AddBomb(){
        maxBombCount++;
    }
}
