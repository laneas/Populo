using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    protected new void Start()
    {
        base.Start();
        //Load additional animations here
    }

    public void attack(Building building)
    {
        int phyDamage = this.atk - building.def;
        if (phyDamage <= 0) { phyDamage = 1; }
        int mgkDamage = this.mgk;
        building.hp = building.hp - (phyDamage + mgkDamage);
    }

    public override void die()
    {
        throw new NotImplementedException();
    }

    public override void select()
    {
        throw new NotImplementedException();
    }
}
