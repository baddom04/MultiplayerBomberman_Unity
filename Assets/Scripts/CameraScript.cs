using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    private Transform player1;
    private Transform player2;
    private Text endText;
    private float originalHeight = 20f;
    private float zOffset = -28f;
    private float reactionTime = 0.5f;
    private void Start() {
        player1 = GameObject.Find("Player1").transform;
        player2 = GameObject.Find("Player2").transform;
        endText = GameObject.Find("EndText").GetComponent<Text>();
        endText.gameObject.SetActive(false);
    }
    void LateUpdate() { 
        Vector3 pos = (player1.position + player2.position) / 2;
        float height = Vector3.Distance(player1.position, player2.position);
        transform.position = new Vector3(
            pos.x, 
            originalHeight + height, 
            pos.z + zOffset
        );
    }
    public void TheEnd(){
        StartCoroutine(RotationAndText());
    }
    private IEnumerator RotationAndText(){
        yield return new WaitForSeconds(reactionTime);
        while(transform.rotation != Quaternion.Euler(0, 0, 0)){
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        endText.gameObject.SetActive(true);
        endText.text = "Congratulations! Press R to restart! Press Q to quit!";
    }
}
