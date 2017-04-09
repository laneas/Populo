using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBuilding : Building
{
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
        //todo
    }

    public override void select()
    {
        throw new NotImplementedException();
    }
}
