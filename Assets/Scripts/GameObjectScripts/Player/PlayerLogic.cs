using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerLogic : MovingObject
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float bombWaitingTime = 3;
    [SerializeField] private List<Bomb> bombs = new List<Bomb>();
    [SerializeField] private int maxBombCount = 1;
    [SerializeField] private int currentBombs = 0;
    [SerializeField] private bool isOnBomb = false;
    [SerializeField] private int bombRadius = 1;
    [SerializeField] private float powerUpDuration = 10f;
    [SerializeField] private bool detonator = false;
    [SerializeField] private float indicatorDuration = 3;
    [SerializeField] public bool IsGhost { get; set; } = false;
    [SerializeField] public bool IsShielded { get; set; } = false;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject ghost;

    void Start()
    {
        CalculatePosition(ref last_i, ref last_j);
    }
    void Update()
    {
        CalculatePosition(ref i, ref j);
        if (CoordinateChanged())
        {
            if (!IsGhost && PlayerGoesWhereItShouldntBe())
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
        return GameController.GetLevel()[i, j] != null;
    }

    public void PlaceBomb()
    {
        if (currentBombs < maxBombCount)
        {
            Bomb b = CreateNewBomb();
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
        foreach (Bomb bs in bombs)
        {
            bs.Detonation();
        }
    }
    private IEnumerator BombCountDown(Bomb b)
    {
        yield return new WaitForSeconds(bombWaitingTime);
        currentBombs--;
        bombs.RemoveAt(0);
        b.Detonation();
    }
    private Bomb CreateNewBomb()
    {
        Bomb b = Instantiate(bombPrefab).GetComponent<Bomb>();
        b.Construct(MiddleOfTile(i, j, 2), i, j, bombRadius);
        return b;
    }

    //*Power-up functions
    public void AddRadius() { bombRadius++; }
    public void AddBomb() { maxBombCount++; }
    public void Detonator() { detonator = true; }
    public void ShieldUp()
    {
        IsShielded = true;
        BubbleUp(shield);
    }
    public void GhostUp()
    {
        IsGhost = true;
        BubbleUp(ghost);
        GetComponent<Collider>().enabled = false;
    }
    public void BubbleUp(GameObject bubble)
    {
        bubble.SetActive(true);
        StartCoroutine(BubbleDown(bubble));
    }
    private IEnumerator BubbleDown(GameObject bubble)
    {
        yield return new WaitForSeconds(powerUpDuration - indicatorDuration);
        bubble.GetComponent<EffectFlickering>().EndIndicator();
        yield return new WaitForSeconds(indicatorDuration);
        if(bubble.CompareTag("Ghost")){
            GetComponent<Collider>().enabled = true;
            if(PlayerGoesWhereItShouldntBe()){
                gameObject.SetActive(false);
                GameController.GameOver();
            } 
            IsGhost = false;
        }
        else if(bubble.CompareTag("Shield")){
            IsShielded = false;
        }
        bubble.SetActive(false);
    }
}
