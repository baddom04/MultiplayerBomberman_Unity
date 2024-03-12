using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public GameObject bombPrefab;
    private float bombWaitingTime = 3;
    [HideInInspector] 
    public int i;
    [HideInInspector] 
    public int j;
    private Rigidbody rb;
    private BombScript bomb;
    [HideInInspector] 
    public bool isBomb = false;
    private int bombRadius = 1;
    [HideInInspector] 
    public int last_i;
    [HideInInspector] 
    public int last_j;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        last_j = (int)Math.Floor((transform.position.x + 45) / 10);
        last_i = 8 - (int)Math.Floor((transform.position.z + 45) / 10);
    }
    void Update()
    {
        i = 8 - (int)Math.Floor((transform.position.z + 45) / 10);
        j = (int)Math.Floor((transform.position.x + 45) / 10);
        if (CoordinateChanged())
        {
            // GameObject.FindWithTag("GameController").GetComponent<GameController>().ChangeTileColors();
            last_i = i;
            last_j = j;
            if (isBomb) bomb.GetComponent<Collider>().isTrigger = false;
        }
    }

    public void Bomb()
    {
        GameObject b = Instantiate(bombPrefab, new Vector3(j * 10 - 40, 2, (8 - i) * 10 - 40), Quaternion.identity);
        bomb = b.GetComponent<BombScript>();
        bomb.i = this.i;
        bomb.j = this.j;
        bomb.radius = this.bombRadius;
        isBomb = true;
        StartCoroutine(BombCountDown());
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
