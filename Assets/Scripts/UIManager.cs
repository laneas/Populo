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
    public GameObject uBuildingInfoPanel;

    //Command Windows
    private List<GameObject> commandPanels = new List<GameObject>();
    public GameObject blankPanel;
    public GameObject castleBuildPanel;
    public GameObject upgradePanel;
    public GameObject characterPanel;
    public GameObject gatherPanel;
    public GameObject levelPanel;


    public bool requestingBuild = false;
    public string buildingType = null;

	// Use this for initialization
	void Start ()
    {
        gm = GetComponentInParent<GameManager>();

        infoPanels.Add(characterInfoPanel);
        infoPanels.Add(uBuildingInfoPanel);

        commandPanels.Add(blankPanel);
        commandPanels.Add(castleBuildPanel);
        commandPanels.Add(upgradePanel);
        commandPanels.Add(characterPanel);
        commandPanels.Add(gatherPanel);
        commandPanels.Add(levelPanel);
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

    public void showLevelPanel()
    {
        hideCommandPanels();
        Character character = gm.leftSelection.GetComponentInParent(typeof(Character)) as Character;
        if (character != null)
        {
            levelPanel.SetActive(true);
            Text points = levelPanel.transform.Find("PointsText").gameObject.GetComponent<Text>();
            Text health = levelPanel.transform.Find("HealthText").gameObject.GetComponent<Text>();
            Text atk = levelPanel.transform.Find("ATKText").gameObject.GetComponent<Text>();
            Text def = levelPanel.transform.Find("DEFText").gameObject.GetComponent<Text>();
            Text mgk = levelPanel.transform.Find("MGKText").gameObject.GetComponent<Text>();
            Text wil = levelPanel.transform.Find("WILText").gameObject.GetComponent<Text>();
            points.text = "Points: "+character.freePoints;
            health.text = "Max Health: " + character.mhp;
            atk.text = "Attack        : " + character.atk;
            def.text = "Defense     : " + character.def;
            mgk.text = "Magic         : " + character.mgk;
            wil.text = "Willpower   : " + character.wil;

            Button wizard = levelPanel.transform.Find("WizardButton").gameObject.GetComponent<Button>();
            Button archer = levelPanel.transform.Find("ArcherButton").gameObject.GetComponent<Button>();
            Button warrior = levelPanel.transform.Find("WarriorButton").gameObject.GetComponent<Button>();
            Button miner = levelPanel.transform.Find("MinerButton").gameObject.GetComponent<Button>();
            Button farmer = levelPanel.transform.Find("FarmerButton").gameObject.GetComponent<Button>();
            Button jack = levelPanel.transform.Find("LumberjackButton").gameObject.GetComponent<Button>();
            Button builder = levelPanel.transform.Find("BuilderButton").gameObject.GetComponent<Button>();

            if (character.type.Equals("Villager"))
            {
                wizard.gameObject.SetActive(true);
                archer.gameObject.SetActive(true);
                warrior.gameObject.SetActive(true);
                miner.gameObject.SetActive(true);
                farmer.gameObject.SetActive(true);
                jack.gameObject.SetActive(true);
                builder.gameObject.SetActive(true);
            }
            else
            {
                wizard.gameObject.SetActive(false);
                archer.gameObject.SetActive(false);
                warrior.gameObject.SetActive(false);
                miner.gameObject.SetActive(false);
                farmer.gameObject.SetActive(false);
                jack.gameObject.SetActive(false);
                builder.gameObject.SetActive(false);
            }
            Color green = new Color(.1f, .68f, .3f);
            Color red = new Color(1.0f, .36f, 0.0f);
            if (character.mgk > 18 && character.wil > 10) { wizard.image.color = green; }
            else { wizard.image.color = red; }
            if (character.atk > 20 && character.wil > 10) { archer.image.color = green; }
            else { archer.image.color = red; }
            if (character.atk > 20 && character.def > 20) { warrior.image.color = green; }
            else { warrior.image.color = red; }
            if (character.def > 15) { miner.image.color = green; }
            else { miner.image.color = red; }
            if (character.atk > 15) { jack.image.color = green; }
            else { jack.image.color = red; }
            farmer.image.color = green;
            builder.image.color = green;
        }
    }

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
            characterInfoPanel.SetActive(true);
            Text type = characterInfoPanel.transform.Find("TypeText").gameObject.GetComponent<Text>();
            Text hp = characterInfoPanel.transform.Find("HPText").gameObject.GetComponent<Text>();
            Text atk = characterInfoPanel.transform.Find("ATKText").gameObject.GetComponent<Text>();
            Text def = characterInfoPanel.transform.Find("DEFText").gameObject.GetComponent<Text>();
            Text mgk = characterInfoPanel.transform.Find("MGKText").gameObject.GetComponent<Text>();
            Text wil = characterInfoPanel.transform.Find("WILText").gameObject.GetComponent<Text>();
            Text spd = characterInfoPanel.transform.Find("SPDText").gameObject.GetComponent<Text>();
            Text rng = characterInfoPanel.transform.Find("RNGText").gameObject.GetComponent<Text>();
            type.text = dBuilding.type;
            hp.text = dBuilding.hp + " / " + dBuilding.mhp;
            atk.text = dBuilding.atk.ToString();
            def.text = dBuilding.def.ToString();
            mgk.text = dBuilding.mgk.ToString();
            wil.text = "---";
            spd.text = dBuilding.spd.ToString();
            rng.text = dBuilding.rng.ToString();
        }
        else if (uBuilding != null)
        {
            uBuildingInfoPanel.SetActive(true);
            Text type = uBuildingInfoPanel.transform.Find("TypeText").gameObject.GetComponent<Text>();
            Text hp = uBuildingInfoPanel.transform.Find("HPText").gameObject.GetComponent<Text>();
            Text def = uBuildingInfoPanel.transform.Find("DEFText").gameObject.GetComponent<Text>();
            Text wood = uBuildingInfoPanel.transform.Find("WoodText").gameObject.GetComponent<Text>();
            Text stone = uBuildingInfoPanel.transform.Find("StoneText").gameObject.GetComponent<Text>();
            Text vil = uBuildingInfoPanel.transform.Find("VilText").gameObject.GetComponent<Text>();
            Text food = uBuildingInfoPanel.transform.Find("FoodText").gameObject.GetComponent<Text>();
            type.text = uBuilding.type;
            hp.text = uBuilding.hp + " / " + uBuilding.mhp;
            def.text = uBuilding.def.ToString();
            wood.text = uBuilding.currentWood+" / "+uBuilding.woodStorage;
            stone.text = uBuilding.currentStone+" / "+uBuilding.stoneStorage;
            vil.text = uBuilding.currentVillagers + " / " + uBuilding.villagerStorage;
            food.text = uBuilding.currentFood + " / " + uBuilding.foodStorage;
        }
    }

    public void setupUpgradePanel()
    {
        Building bui = gm.leftSelection.GetComponent(typeof(Building)) as Building;
        if (bui != null)
        {
            Text wood = upgradePanel.transform.Find("WoodCostText").gameObject.GetComponent<Text>();
            Text stone = upgradePanel.transform.Find("StoneCostText").gameObject.GetComponent<Text>();
            Text villager = upgradePanel.transform.Find("VilCostText").gameObject.GetComponent<Text>();

            if (bui.type.Equals("Wood Wall"))
            {
                wood.text = "Wood Cost: 100";
                stone.text = "Stone Cost: 300";
                villager.text = "Villager Needed: Builder";
            }
            else if (bui.type.Equals("Wood House"))
            {
                wood.text = "Wood Cost: 100";
                stone.text = "Stone Cost: 300";
                villager.text = "Villager Needed: Builder";
            }
            else if (bui.type.Equals("Arrow Tower"))
            {
                wood.text = "Wood Cost: 100";
                stone.text = "Stone Cost: 100";
                villager.text = "Villager Needed: Archer";
            }
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
            case "Level":
                levelPanel.SetActive(true);
                break;
            case "Wood Wall":
                setupUpgradePanel();
                upgradePanel.SetActive(true);
                break;
            case "Wood House":
                setupUpgradePanel();
                upgradePanel.SetActive(true);
                break;
            case "Arrow Tower":
                setupUpgradePanel();
                upgradePanel.SetActive(true);
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
        hideCommandPanels();
        Building bui = gm.leftSelection.GetComponent(typeof(Building)) as Building;
        if (bui != null)
        {
            if (bui.type.Equals("Wood Wall"))
            {
                if (gm.om.addBuilding((int)gm.leftSelection.transform.position.x, (int)gm.leftSelection.transform.position.y, "Stone Wall"))
                {
                    gm.leftSelection.GetComponent<Building>().hp = 0;
                }
            }
            else if (bui.type.Equals("Wood House"))
            {
                gm.om.addBuilding((int)gm.leftSelection.transform.position.x, (int)gm.leftSelection.transform.position.y, "Stone House");
                bui.hp = 0;
            }
            else if (bui.type.Equals("Arrow Tower"))
            {
                gm.om.addBuilding((int)gm.leftSelection.transform.position.x, (int)gm.leftSelection.transform.position.y, "Cannon Tower");
                bui.hp = 0;
            }
        }
    }

    public void levelButtonListener(Button button)
    {
        Character character = gm.leftSelection.GetComponentInParent(typeof(Character)) as Character;
        if(character != null)
        {
            if (button.name.Equals("AddHealth"))
            {
                if (character.freePoints > 0)
                {
                    character.freePoints--;
                    character.mhp++;
                }
                showLevelPanel();
            }
            else if (button.name.Equals("AddAttack"))
            {
                if (character.freePoints > 0)
                {
                    character.freePoints--;
                    character.atk++;
                }
                showLevelPanel();
            }
            else if (button.name.Equals("AddDefense"))
            {
                if (character.freePoints > 0)
                {
                    character.freePoints--;
                    character.def++;
                }
                showLevelPanel();
            }
            else if (button.name.Equals("AddMagic"))
            {
                if (character.freePoints > 0)
                {
                    character.freePoints--;
                    character.mgk++;
                }
                showLevelPanel();
            }
            else if (button.name.Equals("AddWill"))
            {
                if (character.freePoints > 0)
                {
                    character.freePoints--;
                    character.wil++;
                }
                showLevelPanel();
            }
        }
    }

    public void classButtonListener(Button button)
    {
        Villager villager = gm.leftSelection.GetComponentInParent(typeof(Villager)) as Villager;
        if (button.name.Equals("WizardButton"))
        {
            if (button.image.color.r == .1f && button.image.color.g == .68f && button.image.color.b == .3f)
            {
                villager.an.runtimeAnimatorController = Resources.Load("Prefabs/Wizard/Wizard", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
                villager.type = "Wizard";
                villager.cc.radius = 5;
            }
        }
        else if (button.name.Equals("ArcherButton"))
        {
            if (button.image.color.r == .1f && button.image.color.g == .68f && button.image.color.b == .3f)
            {
                villager.an.runtimeAnimatorController = Resources.Load("Prefabs/Archer/Archer", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
                villager.type = "Archer";
                villager.cc.radius = 5;
            }
        }
        else if (button.name.Equals("WarriorButton"))
        {
            if (button.image.color.r == .1f && button.image.color.g == .68f && button.image.color.b == .3f)
            {
                villager.an.runtimeAnimatorController = Resources.Load("Prefabs/Warrior/Warrior", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
                villager.type = "Warrior";
                villager.mhp += 10;
                villager.atk += 10;
                villager.def += 10;
            }
        }
        else if (button.name.Equals("MinerButton"))
        {
            if (button.image.color.r == .1f && button.image.color.g == .68f && button.image.color.b == .3f)
            {
                villager.an.runtimeAnimatorController = Resources.Load("Prefabs/Miner/Miner", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
                villager.type = "Miner";
                villager.stoneCap = 200;
            }
        }
        else if (button.name.Equals("FarmerButton"))
        {
            if (button.image.color.r == .1f && button.image.color.g == .68f && button.image.color.b == .3f)
            {
                villager.an.runtimeAnimatorController = Resources.Load("Prefabs/Farmer/Farmer", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
                villager.type = "Farmer";
                villager.foodCap = 200;
            }
        }
        else if (button.name.Equals("LumberjackButton"))
        {
            if (button.image.color.r == .1f && button.image.color.g == .68f && button.image.color.b == .3f)
            {
                villager.an.runtimeAnimatorController = Resources.Load("Prefabs/Lumberjack/Lumberjack", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
                villager.type = "Lumberjack";
                villager.woodCap = 200;
            }
        }
        else if (button.name.Equals("BuilderButton"))
        {
            if (button.image.color.r == .1f && button.image.color.g == .68f && button.image.color.b == .3f)
            {
                villager.an.runtimeAnimatorController = Resources.Load("Prefabs/Builder/Builder", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
                villager.type = "Builder";
                villager.stoneCap = 100;
                villager.woodCap = 100;
            }
        }
        hideCommandPanels();
    }

    public void characterCommandListener(Button button)
    {
        if (button.name.Equals("UpgradeButton"))
        {
            //switchCommandWindow("Level");
            showLevelPanel();
        }
        else if (button.name.Equals("GatherButton"))
        {
            switchCommandWindow("Gather");
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
            buildingType = "Arrow Tower";
        }
        else
        {
            buildingType = null;
        }
    }

    public void buildCancelButtonListener()
    {
        requestingBuild = false;
        hideCommandPanels();
    }
}
