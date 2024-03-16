using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffectTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionPrefab;
    private void Awake() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform);
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.SetActive(false);
            GameController.gameOn = false;
        }
        if(other.gameObject.CompareTag("Crate")){
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("BlockWall")){
            Destroy(other.gameObject);
        }
    }
}
