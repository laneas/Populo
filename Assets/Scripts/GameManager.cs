using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ObjectManager om;
    public int wave;

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

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        checkMouseClick();
        updatePlayerResources();
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("left click @ " + Input.mousePosition.x + ", " + Input.mousePosition.y);
            leftClick = new global::Node((int)Input.mousePosition.x, (int)Input.mousePosition.y, true);

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag.Equals("Villager"))
                {
                    Debug.Log("Villager was selected");
                    leftSelection = hit.collider.gameObject;
                }
                else if (hit.collider.gameObject.tag.Equals("Monster"))
                {
                    Debug.Log("Monster was selected");
                    leftSelection = hit.collider.gameObject;
                }
                else if (hit.collider.gameObject.tag.Equals("Building"))
                {
                    Debug.Log("Building Selected");
                    leftSelection = hit.collider.gameObject;
                }
                else
                {
                    Debug.Log("Deselect");
                    leftSelection = null;
                }
            }           
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("left click @ " + Input.mousePosition.x + ", " + Input.mousePosition.y);
            leftClick = new global::Node((int)Input.mousePosition.x, (int)Input.mousePosition.y, true);

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag.Equals("Villager"))
                {
                    Debug.Log("Villager was selected");
                    rightSelection = hit.collider.gameObject;
                }
                else if (hit.collider.gameObject.tag.Equals("Monster"))
                {
                    Debug.Log("Monster was selected");
                    rightSelection = hit.collider.gameObject;
                }
                else if (hit.collider.gameObject.tag.Equals("Building"))
                {
                    Debug.Log("Building Selected");
                    rightSelection = hit.collider.gameObject;
                }
                else
                {
                    Debug.Log("Deselect");
                    rightSelection = null;
                }
            }
        }
    }
}
