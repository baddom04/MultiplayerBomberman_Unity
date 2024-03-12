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
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.SetActive(false);
        }
    }
    public void doEffect(){
        ParticleSystem explosion = GetComponentInChildren<ParticleSystem>();
        explosion.Play();
    }
}
