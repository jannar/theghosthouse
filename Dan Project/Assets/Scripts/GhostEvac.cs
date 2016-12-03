using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GhostEvac : MonoBehaviour {

	//objects and scripts
	GameObject corgi;
	GameObject capsule;
	Collider beam;

	//public stuff
	public bool inLineSight = false;
	public TextMesh spoopStatements;
	public Vector3 pointA;
	public Vector3 pointB;
	public float newPointA;
	public float newPointB;

	public List<string> SpoopedWords;

	//private stuff
	private NavMeshAgent agent;
	private int currentSpoopWord;

	// Use this for initialization
	void Start () {

		//gettin' the scrips
		corgi = GameObject.Find ("Player");
		beam = corgi.GetComponentInChildren<CapsuleCollider>();

		//fv = corgi.GetComponent<FieldOfView> ();

		//navmesh
		agent = this.GetComponent<NavMeshAgent>();

		//settin' up the things they say
		int i = 0;
		foreach (string s in SpoopedWords) {
			SpoopedWords [i] = s.Replace ("BREAK", "\r\n");
			i++;
		}
		currentSpoopWord = 0;
		spoopStatements.text = SpoopedWords [currentSpoopWord];
	
	}

	// Update is called once per frame
	void Update () {



//		Vector3 pointA = fv.pointA;
//		Vector3 pointB = fv.pointB;
//
//		float newPointA = pointA.magnitude;
//		float newPointB = pointB.magnitude;
//
//		float Arc = newPointA - newPointB;
//
//		Vector3 newDistance = Vector3.Distance (newPointA, newPointB, this.transform);
//
//		if (ds.visibleGhosts.Contains (this.transform) && this.transform > newDistance) {
//			inLineSight = true;
//		}
//
		//GameObject[] ghostsHere = GameObject.FindGameObjectsWithTag ("Ghost");

//		Vector3 arc = new Vector3 (newPointA.x, newPointB.y, newPointB.z);
//		arc.Normalize;

//		if (this.transform <= newPointA && this.transform >= newPointB) {
//			inLineSight = true;
//		}


//		if (ds.visibleGhosts.Contains (this.transform)) {
//
//			inLineSight = true;
//			this.transform.position -= this.transform.forward * this.agent.speed;
//
//		} else {
//			
//			inLineSight = false;
//			this.agent.speed = originalSpeed;
//		}
		
//		Vector3 targetDirection = corgi.transform.position - this.transform.position; //gets direction of target 
//		float targetAngle = Vector3.Angle(targetDirection,this.transform.forward); //gets angle to target
//		if(Vector3.Distance(corgi.transform.position, transform.position) < spoop.spoopDistance && targetAngle < spoop.spoopAngle){//checks if target is in line of sight
//			ghostDirection *= (-1 * speed * Time.deltaTime);
//			Debug.Log ("Ghost Spoop");
//			}
	
	}

	void OnTriggerEnter(Collider col){

//		Vector3 ghostDirection = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
//		float originalSpeed = agent.speed;

		if (col.CompareTag("Beam") == true) {
			inLineSight = true;
		} else {
			inLineSight = false;
		}

		Debug.Log (col.tag + "COL");
	}
}
