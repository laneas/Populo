using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : Character
{
    public String type;
    public String preReq;
    public int hunger;

    protected new void Start()
    {
        base.Start();
        //Load additional animations here
    } 

    public override void attack(Character character)
    {
        throw new NotImplementedException();
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
