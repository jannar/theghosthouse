using UnityEngine;
using System.Collections;

public class NewPlayerMovement : LivingEntity {

Camera mainCam;

public float lerpSpeed;
public float lerpPercent;
public float speedLimit;
public float speed;
public Vector3 currentPos;
public Vector3 newPos;
public Vector3 target;


	// Use this for initialization
	void Start () {

	mainCam = Camera.main;


	
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentPos = transform.position;
		lerpPercent += Time.deltaTime/lerpSpeed; 
		target = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.y)); 
		Vector3 mousePos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.y)); 
		transform.LookAt (mousePos + Vector3.up * transform.position.y);
		newPos = mousePos + Vector3.up * transform.position.y;
		movement();
	
	}

	void movement () 
	{
		if (Input.GetButton("Move"))
		{
		  	Vector3 targetDirection = target - transform.position;
		  	transform.position += targetDirection * (speed * Time.deltaTime); 
		}
	}
}
