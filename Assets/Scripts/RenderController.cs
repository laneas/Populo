using UnityEngine;
using System.Collections;

public class RenderController : MonoBehaviour {

    private Sprite[] sdvSpring;

    void Awake()
    {
        //Load Sprite Sheets
        sdvSpring = Resources.LoadAll<Sprite>("SpriteSheets/sdv_spring");
    }

	// Use this for initialization
	void Start ()
    {
        drawGrass();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void drawGrass()
    {
        int x = 100;
        int y = 100;
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject obj = new GameObject("Grass");
                SpriteRenderer ren = obj.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                ren.sprite = sdvSpring[156];
                obj.transform.Translate(new Vector2(i, j));
            }
        }
    }
}
