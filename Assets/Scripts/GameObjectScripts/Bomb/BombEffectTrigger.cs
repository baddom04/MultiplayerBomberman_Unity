using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BombEffectTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionPrefab;
    [SerializeField] private GameController gc;
    private void Awake() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity, transform);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            if(!other.gameObject.GetComponent<PlayerLogic>().IsShielded){
                other.gameObject.SetActive(false);
                GameController.GameOver();
            }
        }
        if(other.gameObject.CompareTag("Crate")){
            Destroy(other.gameObject);
        }
    }
}
