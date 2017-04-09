using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<GameObject> villagers;
    public List<GameObject> monsters;
    public List<GameObject> buildings;
    public Pathfinder pf;
	
	void Start ()
    {
        villagers = new List<GameObject>();
        monsters = new List<GameObject>();
        buildings = new List<GameObject>();
        pf = new Pathfinder();

        addVillager(10, 10, "villager");
	}
	
	
	void Update ()
    {
		
	}

    public void addVillager(int x, int y, string type)
    {
        if (type.Equals("villager"))
        {
            GameObject villager = Instantiate(Resources.Load("Prefabs/villagerMale", typeof(GameObject))) as GameObject;
            villager.transform.Translate(new Vector2(x, y));
            villagers.Add(villager);
        }
        //Load others by type here
    }

    public void addMonster(int x, int y, string type)
    {
        //todo
    }

    public void addBuilding(int x, int y, string type)
    {
        //todo
    }

    public void checkCollsions(GameObject one, GameObject two)
    {
        //todo
    }
}
