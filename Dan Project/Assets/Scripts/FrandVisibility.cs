using UnityEngine;
using System.Collections;

public class FrandVisibility : MonoBehaviour {

GameObject corgi; 
SpriteRenderer sr;
CollectingFrands cf;
public bool totallyVisible; 
public bool partiallyVisible;
public Color white;
public Color alphadOut;
public Color lerpingColor;
public float lerpSpeed;
public float detectDistance = 10f; //line of sight distance
public float detectAngle = 360f; //line of sight angle


	// Use this for initialization
	void Start () {


	corgi = GameObject.Find("Player 1");
	sr = GetComponentInChildren<SpriteRenderer>();
	cf = GetComponent<CollectingFrands>();
  
	
	}
	
	// Update is called once per frame
	void Update () {

	if (cf.found == true)
	{
		totallyVisible = true;
	}

	lerpingColor = Color.Lerp(white, alphadOut, Mathf.PingPong(Time.time*lerpSpeed, 1));

	corgiInRange();
	colorCoordinator(); 
	
	}

	void corgiInRange()
	{
		Vector3 targetDirection = corgi.transform.position - this.transform.position; //gets direction of target 
		float targetAngle = Vector3.Angle(targetDirection,this.transform.forward); //gets angle to target

			if(Vector3.Distance(corgi.transform.position, this.transform.position) < detectDistance && targetAngle < detectAngle){ //checks if target is in line of sight
				partiallyVisible = true;

			}
			else
			{
				partiallyVisible = false;
			}
	}

	void colorCoordinator()
	{
		if (totallyVisible == true)
		{
			sr.color = white;
		}

		if (partiallyVisible == true && totallyVisible == false)
		{
			sr.color = lerpingColor;
		}

		if (totallyVisible == false && partiallyVisible == false)
		{
			sr.color = alphadOut;
		}
	}

	void OnTriggerEnter(Collider col){

		//float originalSpeed = agent.speed;

		if (col.CompareTag("Beam") == true) 
		{
			totallyVisible = true;
		} 
		else {
			totallyVisible = false;
		}

		if (col.CompareTag("Nega Beam") == true && cf.found == false)
		{
			totallyVisible = false; 
		}

	}

	void OnTriggerExit(Collider col)
	{
		if (col.CompareTag("Beam") == true && cf.found == false)
		{ 
			totallyVisible = false;
		}
	}

}
