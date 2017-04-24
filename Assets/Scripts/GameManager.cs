using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ObjectManager om;
    public UIManager um;
    public SpriteRenderer nightMask;
    public int wave = 1;
    public int seconds = 0;
    public int hours = 0;

    public GameObject leftSelection = null;
    public Node leftClick = null;
    public GameObject rightSelection = null;
    public Node rightClick = null;

    public int maxWood = 0;
    public int maxStone = 0;
    public int maxFood = 0;
    public int maxVillagers = 0;
    public int currentWood = 0;
    public int currentStone = 0;
    public int currentFood = 0;
    public int currentVillagers = 0;

    private bool waveSpawned = false;
    private bool increaseTimeOn = false;

    // Use this for initialization
    void Start()
    {
        hours = 6;
        wave = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!increaseTimeOn) { StartCoroutine(increaseTime()); }
        adjustLighting();
        checkMouseClick();
        updatePlayerResources();
    }

    public void wipeWave()
    {
        foreach(GameObject obj in om.monsters)
        {
            Monster mon = obj.GetComponent<Monster>();
            mon.hp = 0;
        }
    }

    private void spawnWave(int delay)
    {
        waveSpawned = true;
        int numOfChickens = wave / 1;
        if (numOfChickens < 1) { numOfChickens = 1; }
        int numOfEnts = wave / 2;
        if (numOfEnts < 1) { numOfEnts = 1; }
        int numOfGolems = wave / 3;
        if (numOfGolems < 1) { numOfGolems = 1; }
        int counter = 0;
        for (int i = 0; i < numOfEnts; i++)
        {
            if (counter == 0)
            {
                counter++;
                om.addMonster(0, 90, "Ent");
                om.monsters[om.monsters.Count - 1].GetComponent<Monster>().path.Add(new Node(0, 0, true));
            }
            else if (counter == 1)
            {
                counter++;
                om.addMonster(90, 0, "Ent");
                om.monsters[om.monsters.Count - 1].GetComponent<Monster>().path.Add(new Node(0, 0, true));
            }
            else if (counter == 2)
            {
                counter++;
                om.addMonster(0, -90, "Ent");
                om.monsters[om.monsters.Count - 1].GetComponent<Monster>().path.Add(new Node(0, 0, true));
            }
            else
            {
                counter = 0;
                om.addMonster(-90, 0, "Ent");
                om.monsters[om.monsters.Count - 1].GetComponent<Monster>().path.Add(new Node(0, 0, true));
            }
        }
        for (int i = 0; i < numOfChickens; i++)
        {
            if (counter == 0)
            {
                counter++;
                om.addMonster(0, 90, "Chicken");
                om.monsters[om.monsters.Count - 1].GetComponent<Monster>().path.Add(new Node(0, 0, true));
            }
            else if (counter == 1)
            {
                counter++;
                om.addMonster(90, 0, "Chicken");
                om.monsters[om.monsters.Count - 1].GetComponent<Monster>().path.Add(new Node(0, 0, true));
            }
            else if (counter == 2)
            {
                counter++;
                om.addMonster(0, -90, "Chicken");
                om.monsters[om.monsters.Count - 1].GetComponent<Monster>().path.Add(new Node(0, 0, true));
            }
            else
            {
                counter = 0;
                om.addMonster(-90, 0, "Chicken");
                om.monsters[om.monsters.Count - 1].GetComponent<Monster>().path.Add(new Node(0, 0, true));
            }
        }
        for (int i = 0; i < numOfGolems; i++)
        {
            if (counter == 0)
            {
                counter++;
                om.addMonster(0, 90, "Golem");
                om.monsters[om.monsters.Count - 1].GetComponent<Monster>().path.Add(new Node(0, 0, true));
            }
            else if (counter == 1)
            {
                counter++;
                om.addMonster(90, 0, "Golem");
                om.monsters[om.monsters.Count - 1].GetComponent<Monster>().path.Add(new Node(0, 0, true));
            }
            else if (counter == 2)
            {
                counter++;
                om.addMonster(0, -90, "Golem");
                om.monsters[om.monsters.Count - 1].GetComponent<Monster>().path.Add(new Node(0, 0, true));
            }
            else
            {
                counter = 0;
                om.addMonster(-90, 0, "Golem");
                om.monsters[om.monsters.Count - 1].GetComponent<Monster>().path.Add(new Node(0, 0, true));
            }
        }
    }

    private void adjustLighting()
    {
        Color alpha = nightMask.color;
        switch (this.hours)
        {
            case 19:
            case 5:
                alpha.a = .1f;
                break;
            case 20:
            case 4:
                alpha.a = .20f;
                break;
            case 21:
            case 3:
                alpha.a = .30f;
                break;
            case 22:
            case 2:
                alpha.a = .40f;
                break;
            case 23:
            case 1:
                alpha.a = .50f;
                break;
            case 24:
            case 0:
                alpha.a = .60f;
                break;
            default:
                alpha.a = 0;
                break;
        }
        nightMask.color = alpha;
    }

    IEnumerator increaseTime()
    {
        increaseTimeOn = true;
        yield return new WaitForSeconds(.5f);
        seconds++;
        if (seconds == 30)
        {
            om.heal();
        }
        if (seconds > 59)
        {
            hours++;
            seconds = 0;
        }
        if (hours > 24)
        {
            hours = 0;
        }
        if (hours == 19 && seconds == 0)
        {

        }
        if (hours == 21)
        {
            if (!waveSpawned)
            {
                spawnWave(wave);
            }
        }
        if (hours == 6)
        {
            if(waveSpawned)
            {
                wave++;
                om.populate();
            }
            wipeWave();
            waveSpawned = false;
        }

        increaseTimeOn = false;
    }

    private void updatePlayerResources()
    {
        maxWood = 0;
        maxStone = 0;
        maxFood = 0;
        maxVillagers = 0;
        currentWood = 0;
        currentStone = 0;
        currentFood = 0;
        currentVillagers = 0;
        foreach (GameObject obj in om.buildings)
        {
            UtilityBuilding building = obj.GetComponent(typeof(UtilityBuilding)) as UtilityBuilding;
            if (building != null)
            {
                maxWood += building.woodStorage;
                maxStone += building.stoneStorage;
                maxFood += building.foodStorage;
                maxVillagers += building.villagerStorage;
                currentWood += building.currentWood;
                currentStone += building.currentStone;
                currentFood += building.currentFood;
                currentVillagers += building.currentVillagers;
            }
        }
    }

    private void checkMouseClick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Checks if click was on a UI element
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            //If the click was on a UI element, ignore game logic
            if (results.Count == 0)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                leftClick = new global::Node((int)pos.x, (int)pos.y, true);
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = -1;
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
                RaycastHit2D hit = Physics2D.Raycast(pos, new Vector3(0, 0, -1), 1);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.ToString());
                    if (hit.collider.gameObject.tag.Equals("Villager"))
                    {
                        leftSelection = hit.collider.gameObject;
                        um.showInfoPanel(hit.collider.gameObject);
                        um.switchCommandWindow("Character");
                    }
                    else if (hit.collider.gameObject.tag.Equals("Monster"))
                    {
                        leftSelection = hit.collider.gameObject;
                        um.showInfoPanel(hit.collider.gameObject);
                        um.switchCommandWindow("");
                    }
                    else if (hit.collider.gameObject.tag.Equals("Building"))
                    {
                        leftSelection = hit.collider.gameObject;
                        Building building = hit.collider.gameObject.GetComponent<Building>();
                        um.showInfoPanel(hit.collider.gameObject);
                        um.switchCommandWindow(building.type);
                    }
                    else
                    {
                        //Debug.Log("Deselect");
                        um.switchCommandWindow("");
                        if (um.requestingBuild)
                        {
                            om.addBuilding(leftClick.x, leftClick.y, um.buildingType);
                            um.requestingBuild = false;
                        }
                        leftSelection = null;
                    }
                }
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rightClick = new global::Node((int)pos.x, (int)pos.y, true);
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -1;
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(pos, new Vector3(0, 0, -1), 1);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.ToString());
                if (hit.collider.gameObject.tag.Equals("Villager"))
                {
                    rightSelection = hit.collider.gameObject;
                }
                else if (hit.collider.gameObject.tag.Equals("Monster"))
                {
                    rightSelection = hit.collider.gameObject;
                }
                else if (hit.collider.gameObject.tag.Equals("Building"))
                {
                    rightSelection = hit.collider.gameObject;
                }
                else
                {
                    if (leftSelection != null && leftSelection.gameObject.tag.Equals("Villager"))
                    {
                        leftSelection.gameObject.GetComponent<Villager>().path.Clear();
                        leftSelection.gameObject.GetComponent<Villager>().path.Add(rightClick);
                    }
                }
            }
        }
    }
}
