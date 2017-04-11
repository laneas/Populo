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


	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        checkMouseClick();
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
                Debug.Log(hit.collider.gameObject.GetComponent<Villager>().type);
            }


        }
        if (Input.GetButtonDown("Fire2"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;
            rightClick = new global::Node((int)Input.mousePosition.x, (int)Input.mousePosition.y, true);
            Debug.Log("right click @ " + Input.mousePosition.x + ", " + Input.mousePosition.y);
            
        }
    }
}
