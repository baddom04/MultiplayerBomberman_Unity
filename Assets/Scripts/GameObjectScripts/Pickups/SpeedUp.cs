using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Pickups
{
    protected override void DoEffect(PlayerLogic playerLogic){
        playerLogic.GetComponent<PlayerMovement>().SpeedUp();
    }
}
