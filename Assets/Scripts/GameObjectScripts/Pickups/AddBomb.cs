using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBomb : Pickups
{
    protected override void DoEffect(PlayerLogic playerLogic){
        playerLogic.AddBomb();
    }
}
