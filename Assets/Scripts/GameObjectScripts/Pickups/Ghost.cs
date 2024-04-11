using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Pickups
{
    protected override void DoEffect(PlayerLogic playerLogic){
        playerLogic.GhostUp();
    }
}
