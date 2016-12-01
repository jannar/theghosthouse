using UnityEngine;
using System.Collections;

public class SpoopScript : MonoBehaviour {

public bool spoopingTheDog; 
public float spoopDistance = 10f; //line of sight distance
public float spoopAngle = 360f; //line of sight angle
GameObject corgi; 


	// Use this for initialization
	void Start () 
	{
	corgi = GameObject.Find("Player"); 
	spoopingTheDog = false; 
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	spoopTheDog();

	}

	void spoopTheDog ()
	{
		Vector3 targetDirection = corgi.transform.position - this.transform.position; //gets direction of target 
		float targetAngle = Vector3.Angle(targetDirection,this.transform.forward); //gets angle to target

			if(Vector3.Distance(corgi.transform.position, this.transform.position) < spoopDistance && targetAngle < spoopAngle){ //checks if target is in line of sight
				spoopingTheDog = true;
			}else{
				spoopingTheDog = false;
			}
	}
}
