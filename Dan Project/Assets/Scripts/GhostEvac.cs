using UnityEngine;
using System.Collections;

public class GhostEvac : MonoBehaviour {

	GameObject corgi;
	SpoopScript spoop;
	DogBeenSpooped ds;
	public bool inLineSight = false;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {

		//gettin' the scrips
		corgi = GameObject.Find ("Player");
		spoop = corgi.GetComponent<SpoopScript> ();
		ds = corgi.GetComponent<DogBeenSpooped> ();

		//navmesh
		agent = this.GetComponent<NavMeshAgent>();
	
	}

	// Update is called once per frame
	void Update () {

		Vector3 ghostDirection = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		int originalSpeed = agent.speed;

		if (ds.visibleGhosts.Contains (this.transform)) {

			inLineSight = true;
			this.transform.position -= this.transform.forward * this.agent.speed;

		} else {
			
			inLineSight = false;
			this.agent.speed = originalSpeed;
		}
		
//		Vector3 targetDirection = corgi.transform.position - this.transform.position; //gets direction of target 
//		float targetAngle = Vector3.Angle(targetDirection,this.transform.forward); //gets angle to target
//		if(Vector3.Distance(corgi.transform.position, transform.position) < spoop.spoopDistance && targetAngle < spoop.spoopAngle){//checks if target is in line of sight
//			ghostDirection *= (-1 * speed * Time.deltaTime);
//			Debug.Log ("Ghost Spoop");
//			}
	
	}
}
