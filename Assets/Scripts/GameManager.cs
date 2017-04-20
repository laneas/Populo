using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ObjectManager om;
    public UIManager um;
    public int wave;
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

    private bool increaseTimeOn = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!increaseTimeOn) { StartCoroutine(increaseTime()); }
        checkMouseClick();
        updatePlayerResources();
    }

    private void spawnWave(int delay)
    {
        //Get player resources
        //Spawn enemies according to player status and time
        //set paths
    }

    IEnumerator increaseTime()
    {
        increaseTimeOn = true;
        yield return new WaitForSeconds(1);
        seconds++;
        if (seconds > 60)
        {
            hours++;
            seconds = 0;
        }
        if (hours > 24)
        {
            hours = 0;
        }
        if (hours == 6)
        {
            wave++;
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
                        um.switchCommandWindow("");
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
                    if (leftSelection.gameObject.tag.Equals("Villager"))
                    {
                        leftSelection.gameObject.GetComponent<Villager>().path.Clear();
                        leftSelection.gameObject.GetComponent<Villager>().path.Add(rightClick);
                    }
                }
            }
        }
    }
}
