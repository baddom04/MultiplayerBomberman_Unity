using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRadius : Pickups
{
    protected override void DoEffect(PlayerLogic playerLogic){
        playerLogic.AddRadius();
    }
}
