using UnityEngine;
using System.Collections;

public class FrandSpawning : MonoBehaviour {

public int startingLocation;
public int numberOfStartingLocations; 
public Vector3 startLocation1;
public Vector3 startLocation2;
public Vector3 startLocation3;
public Vector3 startLocation4;

	// Use this for initialization

	void Start () {

	startingLocation = Random.Range (1, numberOfStartingLocations);


	if (startingLocation == 1)
	{
		transform.position = startLocation1;
	}

	if (startingLocation == 2)
	{
		transform.position = startLocation2;
	}

	if (startingLocation == 3)
	{
		transform.position = startLocation3;
	}

	if (startingLocation == 4)
	{
		transform.position = startLocation4;
	}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
