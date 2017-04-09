using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityBuilding : Building
{
    public int woodStorage = 0;
    public int stoneStorage = 0;
    public int foodStorage = 0;
    public int villagerStorage = 0;
    public string preReq = "";


    public override void select()
    {
        throw new NotImplementedException();
    }
}
