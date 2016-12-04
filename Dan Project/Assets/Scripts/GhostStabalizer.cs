using UnityEngine;
using System.Collections;

public class GhostStabalizer : MonoBehaviour {

public Quaternion stablePos;
public float minSpeed;
public float maxSpeed;
public float speedNumber;
NavMeshAgent nma;

	// Use this for initialization
	void Start () 
	{
		
		speedNumber = Random.Range (minSpeed, maxSpeed);
		nma = GetComponent<NavMeshAgent>();
		nma.speed = speedNumber;


	}
	
	// Update is called once per frame
	void Update () 
	{

	transform.rotation = stablePos; 
	
	}
} 
