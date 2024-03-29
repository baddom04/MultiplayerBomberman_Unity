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
        if(other.gameObject.CompareTag("Player")){
            if(!other.gameObject.GetComponent<PlayerLogic>().IsShielded()){
                other.gameObject.SetActive(false);
                // TODO: game-over event
                // GameController.GameOver();
            }
        }
        if(other.gameObject.CompareTag("Crate")){
            Destroy(other.gameObject);
        }
    }
}
