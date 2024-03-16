using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerLogic playerScript = other.gameObject.GetComponent<PlayerLogic>();
            switch (gameObject.tag)
            {
                case "AddRadius":
                    playerScript.IncreaseRadius();
                    break;
                case "AddBomb":
                    playerScript.AddBomb();
                    break;
                case "Speed":
                    other.gameObject.GetComponent<PlayerMovement>().SpeedUp();
                    break;
                case "Shield":
                    playerScript.ShieldUp();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
