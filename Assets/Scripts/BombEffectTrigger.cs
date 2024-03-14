using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffectTrigger : MonoBehaviour
{
    public ParticleSystem explosionPrefab;
    private void Awake() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform);
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.SetActive(false);
            GameObject.FindWithTag("GameController").GetComponent<GameController>().gameOn = false;
        }
        if(other.gameObject.CompareTag("Crate")){
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("BlockWall")){
            Destroy(other.gameObject);
        }
    }
    public void doEffect(){
        ParticleSystem explosion = GetComponentInChildren<ParticleSystem>();
        explosion.Play();
    }
}
