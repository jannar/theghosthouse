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

	}

	void OnTriggerEnter(Collider col){

		Vector3 ghostDirection = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
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
