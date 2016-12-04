using UnityEngine;
using System.Collections;

//!!!!!this is on the corgi character!!!!!

public class EliminateGhosts : MonoBehaviour {

	//bools
	public bool SeesGhost = false;
	public bool borked = false;

	//scripts and objects
	GameObject[] ghosts;
	GhostEvac ge;

	// Use this for initialization
	void Start () {

		//find objects that are ghosts and get their ghost evac scripts
		ghosts = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject go in ghosts){
			
			ge = go.GetComponent<GhostEvac>();

		}
	
	}

	// Update is called once per frame
	void Update () {

		//if you see a ghost you can push the button and make the bork true
		if (SeesGhost == true) {
			if (Input.GetButtonDown ("Fire1")) {
				this.borked = true;
				ge.borked = true;
			}
		} else if (SeesGhost == false){
			borked = false;
			ge.borked = false;
		}
	
	}

	void OnTriggerEnter(Collider col){

		//if a ghost is in range of any of the colliders, you see it
		if (col.CompareTag ("Ghost") == true) {

			SeesGhost = true;
			ge.inLineSight = true;


		} else {

			this.SeesGhost = false;
			ge.inLineSight = false;
		}
			
	}
}
