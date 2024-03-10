using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Transform player2;
    public float originalHeight;
    void Update() { 
        Vector3 pos = (player.position + player2.position) / 2;
        float height = Vector3.Distance(player.position, player2.position);
        transform.position = new Vector3(pos.x, originalHeight + height, pos.z); 
    }
}
