using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//!!!!!this is on the ghost character!!!!!

public class GhostEvac : MonoBehaviour {

	//objects and scripts
	GameObject corgi;

	//public stuff
	public bool inLineSight = false;
	public bool borked = false;
	public TextMesh spoopStatements;

	public List<string> SpoopedWords;

	//private stuff
	private NavMeshAgent agent;
	private int currentSpoopWord;

	// Use this for initialization
	void Start () {

		//gettin' the scrips
		corgi = GameObject.Find ("Player");

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

		if (borked == true) {
			this.gameObject.SetActive (false);
			//gameobject move transform
			//gameobject set active true
		}

	}

	void OnTriggerEnter(Collider col){

		float originalSpeed = agent.speed;

		if (col.CompareTag("Beam") == true) {
			inLineSight = true;
			this.transform.position -= this.transform.forward * this.agent.speed;

		} else {
			inLineSight = false;
			agent.speed = originalSpeed;
		}

	}
}
