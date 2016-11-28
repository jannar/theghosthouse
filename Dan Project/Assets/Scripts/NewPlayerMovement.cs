using UnityEngine;
using System.Collections;

public class NewPlayerMovement :MonoBehaviour {

Camera mainCam;

public float lerpSpeed;
public float lerpPercent;
public Vector3 currentPos;
public Vector3 newPos;


	// Use this for initialization
	void Start () {

	mainCam = Camera.main;

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentPos = transform.position;
		lerpPercent += Time.deltaTime/lerpSpeed; 
		Vector3 mousePos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.transform.position.y)); 
		transform.LookAt (mousePos + Vector3.up * transform.position.y);
		newPos = mousePos + Vector3.up * transform.position.y;
		movement();
	
	}

	void movement () 
	{
		if (Input.GetButton("Move"))
		{
			Debug.Log("Moving");
			transform.position = Vector3.Lerp(currentPos, newPos, lerpPercent);
		}
	}
}
