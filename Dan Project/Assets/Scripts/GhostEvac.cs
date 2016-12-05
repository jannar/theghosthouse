using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//!!!!!this is on the ghost character!!!!!

public class GhostEvac : MonoBehaviour {

	//objects and scripts
	GameObject corgi;
	GhostStabalizer gs; 
	EliminateGhosts eg;
	Enemy en;

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

	//private stuff
	private NavMeshAgent agent;
	private int currentSpoopWord;
	private float randoNumberX;
	private float randoNumberz;

	// Use this for initialization
	void Start () {

		//gettin' the scrips
		corgi = GameObject.Find ("Player 1");

		//navmesh
		agent = GetComponent<NavMeshAgent>();
		gs = GetComponent<GhostStabalizer>();
		eg = corgi.GetComponentInChildren<EliminateGhosts> ();

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

		randoNumberX = Random.Range (minX, maxX);
		randoNumberz = Random.Range (minZ, maxZ);

		if (inLineSight == false || borked == false) {
			agent.speed = originalSpeed;
		}

		if (this.inLineSight == true) {
			this.agent.speed = 0;
		}

		if (this.inLineSight == true && this.agent.speed == 0){
			if (Input.GetButtonDown ("Fire1")) {
				//!!!PLAY BORK SOUND!!!
				eg.borked = true;
			} else {
				eg.borked = false;
			}

		}

		if (eg.borked == true && this.agent.speed == 0 && this.inLineSight == true) {
			this.borked = true;
		}

	}

	void FixedUpdate(){
		if (this.borked == true && eg.borked == true && this.agent.speed == 0 && this.inLineSight == true) {
			moveTheBorkedGhostie (this.transform);
			this.borked = false;
		}
	}

	void OnTriggerEnter(Collider col){

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

		if (col.CompareTag("Nega Beam") == true)
		{
			inLineSight = false; 
			agent.speed = originalSpeed;
			this.borked = false;
			eg.borked = false;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.CompareTag ("Beam")) {
			inLineSight = false;
			this.borked = false;
			eg.borked = false;
		}
	}

	void moveTheBorkedGhostie(Transform ghost){
		Vector3 newLocation = new Vector3 (randoNumberX, this.transform.position.y, randoNumberz);
		this.transform.position = newLocation;
		this.borked = false;
	}
}
