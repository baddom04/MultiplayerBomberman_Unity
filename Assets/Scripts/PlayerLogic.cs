using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public GameObject bombPrefab;
    private float bombWaitingTime = 3;
    private int i;
    private int j;
    private Rigidbody rb;
    private BombScript bomb;
    private bool isBomb = false;
    private int bombRadius = 1;
    private int last_i;
    private int last_j;
    private int gridSize;
    void Start()
    {
        gridSize = GameObject.FindWithTag("GameController").GetComponent<GameController>().gridSize;
        rb = GetComponent<Rigidbody>();
        last_j = (int)((transform.position.x + 5 + (gridSize / 2) * 10) / 10);
        last_i = (gridSize - 1) - (int)((transform.position.z + 5 + (gridSize / 2) * 10) / 10);
    }
    void Update()
    {
        i = (gridSize - 1) - (int)((transform.position.z + 5 + (gridSize / 2) * 10) / 10);
        j = (int)((transform.position.x + 5 + (gridSize / 2) * 10) / 10);
        if (CoordinateChanged())
        {
            Debug.Log(i + " " + j);
            last_i = i;
            last_j = j;
            if (isBomb) bomb.GetComponent<Collider>().isTrigger = false;
        }
    }

    public void PlaceBomb()
    {
        if (!isBomb)
        {
            GameObject b = Instantiate(bombPrefab, new Vector3(j * 10 - (gridSize / 2) * 10, 2, ((gridSize - 1) - i) * 10 - (gridSize / 2) * 10), Quaternion.identity);
            bomb = b.GetComponent<BombScript>();
            bomb.i = this.i;
            bomb.j = this.j;
            bomb.radius = this.bombRadius;
            isBomb = true;
            StartCoroutine(BombCountDown());
        }
    }
    public bool CoordinateChanged()
    {
        return last_i != i || last_j != j;
    }
    private IEnumerator BombCountDown()
    {
        yield return new WaitForSeconds(bombWaitingTime);
        isBomb = false;
        bomb.Detonation();
    }
}
