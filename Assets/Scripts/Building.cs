using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public GameObject model;
    private Rigidbody2D rb;

    public string type = "";
    public int hp = 0;
    public int def = 0;
    public int buildTime = 0;
    public int woodCost = 0;
    public int stoneCost = 0;
    public bool isSelected = false;

	void Start ()
    {
        rb = model.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public abstract void select();
}
