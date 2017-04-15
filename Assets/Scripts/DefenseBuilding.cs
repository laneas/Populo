using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBuilding : Building
{
    public CircleCollider2D range;

    public int mgk = 0;
    public int atk = 0;
    public int spd = 1;
    public int rng = 1;

    // Use this for initialization
    protected new void Start ()
    {
        base.Start();
        //load additional assests here
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void attack(Character character)
    {
        int phyDamage = this.atk - character.def;
        if (phyDamage <= 0) { phyDamage = 1; }
        int mgkDamage = this.mgk - character.wil;
        if (mgkDamage < 0) { mgkDamage = 0; }
        character.hp = character.hp - (phyDamage + mgkDamage);
    }

    public override void select()
    {
        throw new NotImplementedException();
    }
}
