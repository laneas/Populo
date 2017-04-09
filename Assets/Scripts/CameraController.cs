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
        Vector2 move = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        camera.transform.Translate(move);
        camera.orthographicSize += Input.GetAxis("Mouse ScrollWheel");
    }
}
