using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : Character
{
    public string type;
    public string preReq;
    public int hunger;

    protected new void Start()
    {
        base.Start();
        //Load additional animations here
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
