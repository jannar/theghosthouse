using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//!!!!!this is on the ghost character!!!!!

//problems: they don't chase you after you've frozen them twice

public class GhostEvac : MonoBehaviour {

	//objects and scripts
	GameObject corgi;
	GameObject FrandFinder;
	GhostStabalizer gs; 
	EliminateGhosts eg;
	//CollectingFrands cf;

	//public stuff
	public bool inLineSight = false;
	public bool borked = false;

	public TextMesh spoopStatements;

	public float originalSpeed;
	public float minX;
	public float minZ;
	public float maxX;
	public float maxZ;

	public List<string> SpoopedWords;

//	float colliderRadius;
//	float corgiColliderRadius;

	//private stuff
	private NavMeshAgent agent;
	private int currentSpoopWord;
	private float randoNumberX;
	private float randoNumberz;

	// Use this for initialization
	void Start () {

		//gettin' the scrips
		corgi = GameObject.Find ("Player 1");
		//FrandFinder = GameObject.Find ("FrandFinder");

		//navmesh and other scripts
		agent = GetComponent<NavMeshAgent>();
		gs = GetComponent<GhostStabalizer>();
		eg = corgi.GetComponentInChildren<EliminateGhosts> ();
		//cf = corgi.GetComponent<CollectingFrands> ();
		//corgiColliderRadius = corgi.GetComponent<CapsuleCollider>().radius; 
		//colliderRadius = corgi.GetComponentInChild<CapsuleCollider>().radius; 

		//get the original speed
		originalSpeed = Random.Range(gs.minSpeed, gs.maxSpeed);

//		//settin' up the things they say
//		int i = 0;
//		foreach (string s in SpoopedWords) {
//			SpoopedWords [i] = s.Replace ("BREAK", "\r\n");
//			i++;
//		}
//		currentSpoopWord = 0;
//		spoopStatements.text = SpoopedWords [currentSpoopWord];
	
	}

	// Update is called once per frame
	void Update () {

		//generate a random number in these ranges
		randoNumberX = Random.Range (minX, maxX);
		randoNumberz = Random.Range (minZ, maxZ);

		//if no bork button and it's out of view, reset speed
		if (this.borked == false && this.inLineSight == false) {
			agent.speed = originalSpeed;
		}

		//stun if in line of sight
		if (this.inLineSight == true) {
			this.agent.speed = 0;
		}

		//if it's stunned and in the line of sight, play the sound and set
		//the corgi borked to true
		if (this.inLineSight == true && this.agent.speed == 0){
			if (Input.GetButtonDown ("Fire1")) {
				//!!!PLAY BORK SOUND!!!
				eg.borked = true;
			} else {
				eg.borked = false;
			}

		}

		//if bork is pushed, it's frozen, and visible, set this object bork to true
		//i can't believe this is the only way I could get it to work
		if (eg.borked == true && this.agent.speed == 0 && this.inLineSight == true) {
			this.borked = true;
		}

	}

	void FixedUpdate(){

		//if this is borked, the other is borked, it's stunned and visible, move it
		//and reset everything to false, otherwise reset speed
		if (this.borked == true && eg.borked == true && this.agent.speed == 0 && this.inLineSight == true) {
			moveTheBorkedGhostie (this.transform);
			this.borked = false;
		} else if (this.borked == false && this.inLineSight == false) {
			agent.speed = originalSpeed;
		}
	}

	void OnTriggerEnter(Collider col){

		//if this is in the beam, it's frozen, otherwise it's not
		if (col.CompareTag("Beam") == true) 
		{
			inLineSight = true;
			agent.speed = 0;

		} 
		else {
			inLineSight = false;
			agent.speed = originalSpeed; 
			this.borked = false;
			eg.borked = false;
		}

		//if it's left the beam...yeah
		if (col.CompareTag("Nega Beam") == true)
		{
			inLineSight = false; 
			agent.speed = originalSpeed;
			this.borked = false;
			eg.borked = false;
		}
	}

	//leaving the beam, just so that it doesn't remain thinking it's in the beam.
	//beam is impermanence.
	void OnTriggerExit(Collider col){
		if (col.CompareTag ("Beam")) {
			inLineSight = false;
			this.borked = false;
			eg.borked = false;
			agent.speed = originalSpeed;
		}
	}

	//move it
	void moveTheBorkedGhostie(Transform ghost){
		Vector3 newLocation = new Vector3 (randoNumberX, this.transform.position.y, randoNumberz);
		this.transform.position = newLocation;
		agent.speed = originalSpeed;

		//reset destination
//		Vector3 directionToTarget = (corgi.transform.position -transform.position).normalized;  //normalized direction to target
//		Vector3 targetPosition = corgi.transform.position - directionToTarget * (cf.colliderRadius + corgiColliderRadius);//new Vector3(target.position.x, 0, target.position.z); //the target's position
//		agent.SetDestination(targetPosition); //set the destination for the nav mesh agent
	}
}
