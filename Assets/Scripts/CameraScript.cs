using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    private float originalHeight = 20f;
    private float zOffset = -28f;
    void LateUpdate() { 
        Vector3 pos = (player1.position + player2.position) / 2;
        float height = Vector3.Distance(player1.position, player2.position);
        transform.position = new Vector3(
            pos.x, 
            originalHeight + height, 
            pos.z + zOffset
        );
    }
}
