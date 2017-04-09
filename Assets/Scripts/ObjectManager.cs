using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        //addBuilding(5, 1, "Stone Wall");
        //addBuilding(5, 2, "Stone Wall");
        //addBuilding(5, 3, "Stone Wall");
        //addBuilding(5, 4, "Stone Wall");
        //addVillager(1, 2, "Villager");
        //villagers.ElementAt(0).GetComponent<Villager>().path.Add(new Node(10, 2, true));
    }
	
	
	void Update ()
    {
		
	}

    public void addVillager(int x, int y, string type)
    {
        if (type.Equals("Villager"))
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

    //***NOTE: We will need to later check the difference in y and adjust z so that walls appear behind when >y and <y when in front
    public void addBuilding(int x, int y, string type)
    {
        if(type.Equals("Wood Wall"))
        {
            GameObject building = Instantiate(Resources.Load("Prefabs/wallWood", typeof(GameObject))) as GameObject;
            building.transform.Translate(new Vector2(x, y));
            buildings.Add(building);
        }
        else if (type.Equals("Stone Wall"))
        {
            GameObject building = Instantiate(Resources.Load("Prefabs/wallStone", typeof(GameObject))) as GameObject;
            building.transform.Translate(new Vector2(x, y));
            buildings.Add(building);
        }
    }

    public void checkCollsions(GameObject one, GameObject two)
    {
        //todo
    }
}
