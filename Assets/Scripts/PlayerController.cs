using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public string up;
    public string down;
    public string left;
    public string right;
    public GameObject bombPrefab;
    public float bombWaitingTime;
    [HideInInspector] public int i;
    [HideInInspector] public int j;
    private Rigidbody rb;
    private BombScript bomb;
    [HideInInspector] public bool isBomb = false;
    private int bombRadius;
    private int last_i;
    private int last_j;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        last_i = (int)Math.Floor((transform.position.x + 45) / 10);
        last_j = 8 - (int)Math.Floor((transform.position.z + 45) / 10);
        bombRadius = 1;
    }
    void Update()
    {
        // float moveVertical = Input.GetAxis("Vertical");
        // float moveHorizontal = Input.GetAxis("Horizontal");

        // if(MoveCondition()){
        //     rb.velocity = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;
        // }
        if(Input.GetKey(left)) rb.velocity = Vector3.left * speed; 
        if(Input.GetKey(right)) rb.velocity = Vector3.right * speed; 
        if(Input.GetKey(up)) rb.velocity = Vector3.forward * speed; 
        if(Input.GetKey(down)) rb.velocity = Vector3.back * speed; 

        i = 8 - (int)Math.Floor((transform.position.z + 45) / 10);
        j = (int)Math.Floor((transform.position.x + 45) / 10);
        if (CoordinateChanged())
        {
            last_i = i;
            last_j = j;
            if (isBomb) bomb.GetComponent<Collider>().isTrigger = false;
        }
    }
    private bool MoveCondition(){
        return Input.GetKey(left) || Input.GetKey(up) || Input.GetKey(down) || Input.GetKey(right);
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
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "BombEffect")
        {
            this.gameObject.SetActive(false);
        }
    }
}
