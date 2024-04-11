using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : Pickups
{
    protected override void DoEffect(PlayerLogic playerLogic){
        playerLogic.Detonator();
    }
}
