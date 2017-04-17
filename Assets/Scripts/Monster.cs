using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    public bool isAttackingBuilding = false;

    protected new void Start()
    {
        base.Start();
        //Load additional animations here
    }

    public void attack(Building building)
    {
        if (!isAttackingBuilding)
        {
            IEnumerator attack = attackPrep(building);
            StartCoroutine(attack);
        }
    }

    public IEnumerator attackPrep(Building building)
    {
        while (true)
        {
            isAttackingBuilding = true;
            yield return new WaitForSeconds((float)spd);
            int phyDamage = this.atk - building.def;
            if (phyDamage <= 0) { phyDamage = 1; }
            int mgkDamage = this.mgk;
            building.hp = building.hp - (phyDamage + mgkDamage);
            isAttackingBuilding = false;
        }
    }

    public override void die()
    {
        Destroy(this.model);
    }

    public override void select()
    {
        throw new NotImplementedException();
    }
}
