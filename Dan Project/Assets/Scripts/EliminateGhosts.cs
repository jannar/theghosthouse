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
	AudioSource source;
	public AudioClip bark;

	// Use this for initialization
	void Start () {

		//find objects that are ghosts and get their ghost evac scripts
		ghosts = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject go in ghosts){

			ge = go.GetComponent<GhostEvac>();

		}

		//get the sound
		source = GetComponentInParent<AudioSource>();

	}

	void Update(){

		if (Input.GetButtonDown ("Fire1")) {
			source.PlayOneShot (bark);
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
