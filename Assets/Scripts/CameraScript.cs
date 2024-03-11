using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float originalHeight;
    void LateUpdate() { 
        Vector3 pos = (player1.position + player2.position) / 2;
        float height = Vector3.Distance(player1.position, player2.position);
        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, pos.x, 0.5f), 
            Mathf.Lerp(transform.position.y, originalHeight + height, 1f), 
            Mathf.Lerp(transform.position.z, pos.z, 0.5f)
        );
    }
}
