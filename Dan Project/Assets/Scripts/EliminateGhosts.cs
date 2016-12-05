using UnityEngine;
using System.Collections;

//!!!!!this is on the corgi character!!!!!

public class EliminateGhosts : MonoBehaviour {

	//bools
	public bool SeesGhost = false;
	public bool borked = false;
	//LOAD BORK SOUNDS!!!

	//scripts and objects
	GameObject[] ghosts;
	GhostEvac ge;
	//SOMETHING AUDIO MANAGER TOO PROBABLY

	// Use this for initialization
	void Start () {

		//find objects that are ghosts and get their ghost evac scripts
		ghosts = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject go in ghosts){

			ge = go.GetComponent<GhostEvac>();

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
