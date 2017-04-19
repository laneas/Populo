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
        //addBuilding(10, 10, "Stone House");
        //addVillager(4, 4, "Villager");
        //villagers.ElementAt(0).GetComponent<Villager>().path.Add(new Node(4, 8, true));
        //villagers.ElementAt(0).GetComponent<Villager>().path.Add(new Node(8, 8, true));
        //villagers.ElementAt(0).GetComponent<Villager>().path.Add(new Node(8, 4, true));
        //addBuilding(1, 7, "Stone House");
        //addMonster(1, -5, "Ent");
        //monsters.ElementAt(0).GetComponent<Monster>().path.Add(new global::Node(1, 10, true));
    }
	
	
	void Update ()
    {
        checkCollsions();
        removeDead();
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
        if (type.Equals("Ent"))
        {
            GameObject monster = Instantiate(Resources.Load("Prefabs/Ent", typeof(GameObject))) as GameObject;
            monster.transform.Translate(new Vector2(x, y));
            monsters.Add(monster);
        }
    }

    //***NOTE: We will need to later check the difference in y and adjust z so that walls appear behind when >y and <y when in front
    public void addBuilding(int x, int y, string type)
    {
        if (type.Equals("Wood Wall"))
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
        else if (type.Equals("Wood House"))
        {
            GameObject building = Instantiate(Resources.Load("Prefabs/housesWood", typeof(GameObject))) as GameObject;
            building.transform.Translate(new Vector2(x, y));
            buildings.Add(building);
        }
        else if (type.Equals("Stone House"))
        {
            GameObject building = Instantiate(Resources.Load("Prefabs/housesStone", typeof(GameObject))) as GameObject;
            building.transform.Translate(new Vector2(x, y));
            buildings.Add(building);
        }
        else if (type.Equals("Fire Tower"))
        {
            GameObject building = Instantiate(Resources.Load("Prefabs/flametower", typeof(GameObject))) as GameObject;
            building.transform.Translate(new Vector2(x, y));
            buildings.Add(building);
        }
    }

    private void checkCollsions()
    {
        // Villagers x Monsters
        foreach (GameObject vilObj in villagers)
        {
            Villager villager = vilObj.GetComponent(typeof (Villager)) as Villager;
            BoxCollider2D villagerBounds = vilObj.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
            CircleCollider2D villagerRange = vilObj.GetComponentInChildren(typeof(CircleCollider2D)) as CircleCollider2D;
            foreach (GameObject monObj in monsters)
            {
                Monster monster = monObj.GetComponent(typeof(Monster)) as Monster;
                BoxCollider2D monsterBounds = monObj.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
                CircleCollider2D monsterRange = monObj.GetComponentInChildren(typeof(CircleCollider2D)) as CircleCollider2D;
                if (villagerRange.bounds.Intersects(monsterBounds.bounds))
                {
                    villager.attack(monster);
                }
            }
        }
        // Monsters x Villagers
        foreach (GameObject monObj in monsters)
        {
            Monster monster = monObj.GetComponent(typeof(Monster)) as Monster;
            BoxCollider2D monsterBounds = monObj.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
            CircleCollider2D monsterRange = monObj.GetComponentInChildren(typeof(CircleCollider2D)) as CircleCollider2D;
            foreach (GameObject vilObj in villagers)
            {
                Villager villager = vilObj.GetComponent(typeof(Villager)) as Villager;
                BoxCollider2D villagerBounds = vilObj.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
                CircleCollider2D villagerRange = vilObj.GetComponentInChildren(typeof(CircleCollider2D)) as CircleCollider2D;
                if (monsterRange.bounds.Intersects(villagerBounds.bounds))
                {
                    monster.attack(villager);
                }
            }
        }
        // Buildings x Monsters
        foreach (GameObject buiObj in buildings)
        {
            DefenseBuilding building = buiObj.GetComponent(typeof(DefenseBuilding)) as DefenseBuilding;
            BoxCollider2D buildingBounds = buiObj.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
            CircleCollider2D buildingRange = buiObj.GetComponentInChildren(typeof(CircleCollider2D)) as CircleCollider2D;//buiObj.GetComponent(typeof(CircleCollider2D)) as CircleCollider2D;

            if (building != null)
            {
                foreach (GameObject monObj in monsters)
                {
                    Monster monster = monObj.GetComponent(typeof(Monster)) as Monster;
                    BoxCollider2D monsterBounds = monObj.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
                    CircleCollider2D monsterRange = monObj.GetComponentInChildren(typeof(CircleCollider2D)) as CircleCollider2D;
                    if (buildingRange.bounds.Intersects(monsterBounds.bounds))
                    {
                        building.attack(monster);
                    }
                }
            }
        }
        // Monsters x Buildings
        foreach (GameObject monObj in monsters)
        {
            Monster monster = monObj.GetComponent(typeof(Monster)) as Monster;
            BoxCollider2D monsterBounds = monObj.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
            CircleCollider2D monsterRange = monObj.GetComponentInChildren(typeof(CircleCollider2D)) as CircleCollider2D;
            foreach (GameObject buiObj in buildings)
            {
                Building building = buiObj.GetComponent(typeof(Building)) as Building;
                BoxCollider2D buildingBounds = buiObj.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
                CircleCollider2D buildingRange = buiObj.GetComponentInChildren(typeof(CircleCollider2D)) as CircleCollider2D;
                if (building != null && monsterRange.bounds.Intersects(buildingBounds.bounds))
                {
                    monster.attack(building);
                }
            }
        }
    }

    private void removeDead()
    {
        List<GameObject> deadVillagers = new List<GameObject>();
        List<GameObject> deadMonsters = new List<GameObject>();
        List<GameObject> deadBuildings = new List<GameObject>();

        //Collect Dead
        foreach (GameObject vilObj in villagers)
        {
            if (vilObj.GetComponent<Villager>().hp <= 0)
            {
                deadVillagers.Add(vilObj);
            }
        }
        foreach (GameObject monObj in monsters)
        {
            if(monObj.GetComponent<Monster>().hp <= 0)
            {
                deadMonsters.Add(monObj);
            }
        }
        foreach(GameObject buiObj in buildings)
        {
            if (buiObj.GetComponent<Building>().hp <= 0)
            {
                deadBuildings.Add(buiObj);
            }
        }

        //Remove Dead
        foreach(GameObject vilObj in deadVillagers)
        {
            villagers.Remove(vilObj);
            Destroy(vilObj);
        }
        foreach (GameObject monObj in deadMonsters)
        {
            monsters.Remove(monObj);
            Destroy(monObj);
        }
        foreach (GameObject buiObj in deadBuildings)
        {
            buildings.Remove(buiObj);
            Destroy(buiObj);
        }
    }
}
