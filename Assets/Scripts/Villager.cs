using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : Character
{
    public string preReq;
    public int hunger;

    public int currentFood = 0;
    public int foodCap = 0;
    public int currentWood = 0;
    public int woodCap = 0;
    public int currentStone = 0;
    public int stoneCap = 0;

    public string gatherZoneType = "";
    public bool inGatherZone = false;
    public bool isReturning = false;
    public bool isGathering = false;

    protected new void Start()
    {
        base.Start();
        //Load additional animations here
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
        returnLoad();
        if (inGatherZone)
        {
            gather(gatherZoneType);
        }
    }

    public void returnLoad()
    {
        if (woodCap == currentWood || stoneCap == currentStone || foodCap == currentFood)
        {
            if (!isReturning)
            {
                path.Clear();
                path.Add(new global::Node(0, 0, true));
            }
            isReturning = true;
        }
        else
        {
            isReturning = false;
        }
    }

    public void gather(string material)
    {
        if (!isGathering)
        {
            IEnumerator gather = gatherPrep(material);
            StartCoroutine(gather);
        }
    }

    public IEnumerator gatherPrep(string material)
    {
        //while (true)
        //{
            isGathering = true;
            yield return new WaitForSeconds((float)spd * 2);
            if (material.Equals("Wood") && currentWood < woodCap)
            {
                currentWood += this.atk;
                if (currentWood > woodCap) { currentWood = woodCap; }
            }
            else if (material.Equals("Stone") && currentStone < stoneCap)
            {
                currentStone += this.atk;
                if (currentStone > stoneCap) { currentStone = stoneCap; }
            }
            else if (material.Equals("Food") && currentFood < foodCap)
            {
                currentFood += this.atk;
                if (currentFood > foodCap) { currentFood = foodCap; }
            }
            else
            {
                //ignore
            }
            addXP(1);
            isGathering = false;
        //}
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
