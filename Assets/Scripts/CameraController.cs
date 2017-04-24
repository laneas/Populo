using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Camera camera;
    public int speed = 1;

	// Use this for initialization
	void Start ()
    {
        camera = Camera.main;
	}
	
	/*
     * Update is called once per frame
     * Gets input form ASDF keys and scroll button.
     * Translate's camera by input, if any. 
     */
	void Update ()
    {
        if (camera.transform.position.x > 90)
        {
            Vector3 v = camera.transform.position;
            v.x = 89;
            camera.transform.position = v;
        }
        if (camera.transform.position.x < -90)
        {
            Vector3 v = camera.transform.position;
            v.x = -89;
            camera.transform.position = v;
        }
        if (camera.transform.position.y > 90)
        {
            Vector3 v = camera.transform.position;
            v.y = 89;
            camera.transform.position = v;
        }
        if (camera.transform.position.y < -90)
        {
            Vector3 v = camera.transform.position;
            v.y = -89;
            camera.transform.position = v;
        }
        if (camera.orthographicSize > 25)
        {
            camera.orthographicSize = 25;
        }
        if (camera.orthographicSize < 10)
        {
            camera.orthographicSize = 10;
        }

        if (camera.transform.position.x <= 90 && camera.transform.position.x >= -90)
        {
            if (camera.transform.position.y <= 90 && camera.transform.position.y >= -90)
            {
                Vector2 move = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
                camera.transform.Translate(move);
                camera.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * -2;
            } 
        }     
        
    }
}
