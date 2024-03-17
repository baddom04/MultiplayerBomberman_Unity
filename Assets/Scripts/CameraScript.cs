using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    private Transform player1;
    private Transform player2;
    private float originalHeight = 20f;
    private float zOffset = 10 - GameController.gridSize * 5;
    private float reactionTime = 0.5f;
    private float lowerYBound = -20 + GameController.gridSize * 10;
    private float upperYBound = GameController.gridSize * 10;
    private float lowerZBound = -60;
    private void Start()
    {
        player1 = GameObject.Find("Player1").transform;
        player2 = GameObject.Find("Player2").transform;
    }
    void LateUpdate()
    {
        Vector3 pos = (player1.position + player2.position) / 2;
        float height = Vector3.Distance(player1.position, player2.position);
        transform.position = Vector3.Lerp(transform.position, new Vector3(
            pos.x,
            originalHeight + height,
            pos.z + zOffset
        ), 0.05f);
        if (transform.position.y < lowerYBound)
            transform.position = new Vector3(transform.position.x, lowerYBound, transform.position.z);
        if (transform.position.y > upperYBound)
            transform.position = new Vector3(transform.position.x, upperYBound, transform.position.z);
        if (transform.position.z < lowerZBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, lowerZBound);
    }
    public void TheEnd()
    {
        StartCoroutine(RotationAndText());
    }
    private IEnumerator RotationAndText()
    {
        yield return new WaitForSeconds(reactionTime);
        while (transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
