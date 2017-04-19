using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gm;

    public Text timeUI;

    public GameObject castleBuildPanel;


    public bool requestingBuild = false;
    public string buildingType = null;

	// Use this for initialization
	void Start ()
    {
        gm = GetComponentInParent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        updateTime();
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
            buildingType = "Flame Tower";
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

    private void updateTime()
    {
        string hoursF = gm.hours.ToString();
        string secondsF = gm.seconds.ToString();
        if (gm.hours < 10) { hoursF = "0" + hoursF; }
        if (gm.seconds < 10) { secondsF = "0" + secondsF; }
        string timeString = hoursF + ":" + secondsF;
        timeUI.text = timeString;
    }
}
