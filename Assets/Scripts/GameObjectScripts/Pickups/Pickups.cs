using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickups : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DoEffect(other.gameObject.GetComponent<PlayerLogic>());
            Destroy(gameObject);
        }
    }
    protected abstract void DoEffect(PlayerLogic playerLogic);
}
