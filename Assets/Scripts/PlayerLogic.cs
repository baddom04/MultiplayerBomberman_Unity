using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MovingObject
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float bombWaitingTime = 3;
    [SerializeField] private List<BombScript> bombs = new List<BombScript>();
    [SerializeField] private int maxBombCount = 1;
    [SerializeField] private int currentBombs = 0;
    [SerializeField] private bool isOnBomb = false;
    [SerializeField] private int bombRadius = 1;
    [SerializeField] private float powerUpDuration = 10f;
    [SerializeField] public GameObject shield;
    [SerializeField] private bool detonator = false;
    [SerializeField] private float indicatorDuration = 3;
    void Start()
    {
        CalculatePosition(ref last_i, ref last_j);
    }
    void Update()
    {
        CalculatePosition(ref i, ref j);
        if (CoordinateChanged())
        {
            if (PlayerGoesWhereItShouldntBe())
            {
                transform.position = MiddleOfTile(last_i, last_j, 0);
            }
            else
            {
                UpdatePosition();
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
            BombScript b = CreateNewBomb();
            bombs.Add(b);
            currentBombs++;
            isOnBomb = true;
            if (!detonator) StartCoroutine(BombCountDown(b));
        }
        else if (detonator)
        {
            DetonateAllBombs();
            currentBombs = 0;
            isOnBomb = false;
            bombs.Clear();
        }
    }
    private void DetonateAllBombs()
    {
        foreach (BombScript bs in bombs)
        {
            bs.Detonation();
        }
    }
    private IEnumerator BombCountDown(BombScript bs)
    {
        yield return new WaitForSeconds(bombWaitingTime);
        currentBombs--;
        bombs.RemoveAt(0);
        bs.Detonation();
    }
    private BombScript CreateNewBomb()
    {
        BombScript b = Instantiate(bombPrefab, MiddleOfTile(i, j, 2), Quaternion.identity).GetComponent<BombScript>();
        b.i = i;
        b.j = j;
        b.radius = bombRadius;
        return b;
    }

    //*Power-up functions
    public void EffectDown(string name){
        switch(name){
            case "Shield":
                ShieldDown();
                break;
        }
    }
    public void IncreaseRadius() { bombRadius++; }
    public void AddBomb() { maxBombCount++; }
    public void Detonator() { detonator = true; }
    public bool IsShielded() { return shield.activeInHierarchy; }
    public void ShieldUp()
    {
        shield.SetActive(true);
        StartCoroutine(ShieldDown());
    }
    private IEnumerator ShieldDown()
    {
        yield return new WaitForSeconds(powerUpDuration - indicatorDuration);
        shield.GetComponent<EffectFlickering>().EndIndicator();
        yield return new WaitForSeconds(indicatorDuration);
        shield.SetActive(false);
    }
}
