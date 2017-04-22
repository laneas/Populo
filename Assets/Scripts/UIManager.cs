using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gm;

    //Resource Windows
    public Text timeUI;
    public Text woodUI;
    public Text stoneUI;
    public Text foodUI;
    public Text popUI;

    //Info Windows
    private List<GameObject> infoPanels = new List<GameObject>();
    public GameObject characterInfoPanel;

    //Command Windows
    private List<GameObject> commandPanels = new List<GameObject>();
    public GameObject blankPanel;
    public GameObject castleBuildPanel;
    public GameObject upgradePanel;
    public GameObject characterPanel;
    public GameObject gatherPanel;


    public bool requestingBuild = false;
    public string buildingType = null;

	// Use this for initialization
	void Start ()
    {
        gm = GetComponentInParent<GameManager>();

        infoPanels.Add(characterInfoPanel);

        commandPanels.Add(blankPanel);
        commandPanels.Add(castleBuildPanel);
        commandPanels.Add(upgradePanel);
        commandPanels.Add(characterPanel);
        commandPanels.Add(gatherPanel);
	}
	
	// Update is called once per frame
	void Update ()
    {
        updateTime();
        updateResouces();
	}
    
    //-------------------------Update Driven Events-----------------------------------------------

    private void updateResouces()
    {
        woodUI.text = gm.currentWood.ToString();
        stoneUI.text = gm.currentStone.ToString();
        foodUI.text = gm.currentFood.ToString();
        popUI.text = gm.currentVillagers.ToString();
    }

    private void updateTime()
    {
        string hoursF = gm.hours.ToString();
        string secondsF = gm.seconds.ToString();
        if (gm.hours < 10) { hoursF = "0" + hoursF; }
        if (gm.seconds < 10) { secondsF = "0" + secondsF; }
        string timeString = hoursF + ":" + secondsF;
        timeUI.text = timeString;
    }

    //---------------------Input Listeners------------------------------------

    public void showInfoPanel(GameObject obj)
    {
        hideInfoPanels();
        Character character = obj.GetComponent(typeof(Character)) as Character;
        DefenseBuilding dBuilding = obj.GetComponent(typeof(DefenseBuilding)) as DefenseBuilding;
        UtilityBuilding uBuilding = obj.GetComponent(typeof(UtilityBuilding)) as UtilityBuilding;
        if (character != null)
        {
            characterInfoPanel.SetActive(true);
            Text type = characterInfoPanel.transform.Find("TypeText").gameObject.GetComponent<Text>();
            Text hp = characterInfoPanel.transform.Find("HPText").gameObject.GetComponent<Text>();
            Text atk = characterInfoPanel.transform.Find("ATKText").gameObject.GetComponent<Text>();
            Text def = characterInfoPanel.transform.Find("DEFText").gameObject.GetComponent<Text>();
            Text mgk = characterInfoPanel.transform.Find("MGKText").gameObject.GetComponent<Text>();
            Text wil = characterInfoPanel.transform.Find("WILText").gameObject.GetComponent<Text>();
            Text spd = characterInfoPanel.transform.Find("SPDText").gameObject.GetComponent<Text>();
            Text rng = characterInfoPanel.transform.Find("RNGText").gameObject.GetComponent<Text>();
            type.text = character.type;
            hp.text = character.hp+" / "+character.mhp;
            atk.text = character.atk.ToString();
            def.text = character.def.ToString();
            mgk.text = character.mgk.ToString();
            wil.text = character.wil.ToString();
            spd.text = character.spd.ToString();
            rng.text = character.rng.ToString();
        }
        else if (dBuilding != null)
        {

        }
        else if (uBuilding != null)
        {

        }
    }

    private void hideInfoPanels()
    {
        foreach (GameObject panel in infoPanels)
        {
            panel.SetActive(false);
        }
    }

    public void switchCommandWindow(string type)
    {
        hideCommandPanels();
        switch (type)
        {
            case "Castle":
                castleBuildPanel.SetActive(true);
                break;
            case "Character":
                characterPanel.SetActive(true);
                break;
            case "Gather":
                gatherPanel.SetActive(true);
                break;
            default:
                blankPanel.SetActive(true);
                break;
        }
    }

    public void hideCommandPanels()
    {
        foreach (GameObject panel in commandPanels)
        {
            panel.SetActive(false);
        }
    }

    public void upgradeButtonListener()
    {
        Debug.Log("Upgrade");
    }

    public void characterCommandListener(Button button)
    {
        if (button.name.Equals("MoveButton"))
        {
            
        }
        else if (button.name.Equals("GatherButton"))
        {
            switchCommandWindow("Gather");
        }
        else if (button.name.Equals("AttackButton"))
        {
            
        }
    }

    public void gatherButtonListener(Button button)
    {
        Villager villager = gm.leftSelection.GetComponent(typeof(Villager)) as Villager;
        if (villager != null)
        {
            villager.gatherZoneType = "";
            villager.inGatherZone = false;
            villager.isGathering = false;
            if (button.name.Equals("WoodButton"))
            {
                villager.path.Clear();
                villager.path.Add(new Node(90, 0, true));
            }
            else if (button.name.Equals("StoneButton"))
            {
                villager.path.Clear();
                villager.path.Add(new Node(-90, 0, true));
            }
            else if (button.name.Equals("FoodButton"))
            {
                villager.path.Clear();
                villager.path.Add(new Node(0, 90, true));
            }
        }
    }

    public void buildButtonListener(Button button)
    {
        requestingBuild = true;
        if (button.name.Equals("BuildHouseButton"))
        {
            buildingType = "Wood House";
        }
        else if (button.name.Equals("BuildWallButton"))
        {
            buildingType = "Wood Wall";
        }
        else if (button.name.Equals("BuildTowerButton"))
        {
            buildingType = "Fire Tower";
        }
        else
        {
            buildingType = null;
        }
    }

    public void buildCancelButtonListener()
    {
        requestingBuild = false;
    }
}
