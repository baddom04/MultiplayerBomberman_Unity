using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float bombWaitingTime = 3;
    [SerializeField] private int i;
    [SerializeField] private int j;
    private Rigidbody rb;
    private List<BombScript> bombs = new List<BombScript>();
    [SerializeField] private int maxBombCount = 1;
    [SerializeField] private int currentBombs = 0;
    [SerializeField] private bool isOnBomb = false;
    [SerializeField] private int bombRadius = 1;
    [SerializeField] private float powerUpDuration = 10f;
    [SerializeField] private GameObject shield;
    [SerializeField] private bool detonator = false;
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
                if (isOnBomb)
                {
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
            if(!detonator) StartCoroutine(BombCountDown(b));
        }
        else{
            currentBombs = 0;
            foreach(BombScript bs in bombs){
                bs.Detonation();
            }
            isOnBomb = false;
            bombs.Clear();
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
    public void IncreaseRadius() { bombRadius++; }
    public void AddBomb() { maxBombCount++; }
    public void Detonator(){ detonator = true; }
    public bool IsShielded() { return shield.activeInHierarchy; }
    public void ShieldUp()
    {
        shield.SetActive(true);
        StartCoroutine(ShieldDown());
    }
    IEnumerator ShieldDown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        shield.SetActive(false);
    }
}
