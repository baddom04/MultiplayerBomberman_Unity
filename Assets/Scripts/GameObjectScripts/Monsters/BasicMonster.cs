using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BasicMonster : MovingObject
{
    [SerializeField] private Vector3 heading;
    [SerializeField]
    private Vector3[] directions = new Vector3[]{
        Vector3.forward,
        Vector3.back,
        Vector3.left,
        Vector3.right
    };
    [SerializeField] private float speed = 10;
    [SerializeField] private float randomDirectionChance = 1f;
    [SerializeField] private float allowedRange = 0.1f;
    [SerializeField] private bool changedDirection = false;
    void Awake()
    {
        CalculatePosition(ref i, ref j);
        heading = directions[Random.Range(0, directions.Length)];
    }
    private void Update()
    {
        transform.Translate(heading * speed * Time.deltaTime);
        CalculatePosition(ref i, ref j);
        if (CoordinateChanged()){
            UpdatePosition();
            changedDirection = false;
        } 
        if(OnTileCenter() && CalculateChance(randomDirectionChance) && !changedDirection){
            heading = directions[Random.Range(0, directions.Length)];
            changedDirection = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!other.gameObject.GetComponent<PlayerLogic>().IsShielded)
            {
                other.gameObject.SetActive(false);
                GameController.GameOver();
            }
        }
        else if (!other.gameObject.CompareTag("Monster"))
        {
            Debug.Log(other.gameObject.tag);
            transform.position = MiddleOfTile(i, j, transform.position.y);
            heading = directions[Random.Range(0, directions.Length)];
        }
    }
    private bool CalculateChance(float chance)
    {
        return Random.Range(0, (int)(1 / chance)) == 0;
    }
    private bool OnTileCenter(){
        return Vector3.Distance(transform.position, MiddleOfTile(i, j, transform.position.y))<=allowedRange;
    }
}
