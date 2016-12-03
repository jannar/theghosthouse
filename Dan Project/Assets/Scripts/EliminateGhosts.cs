using UnityEngine;
using System.Collections;

public class EliminateGhosts : MonoBehaviour {

	public bool SeesGhost = false;

	GameObject[] ghosts;
	GhostEvac ge;

	// Use this for initialization
	void Start () {

		ghosts = GameObject.FindGameObjectsWithTag ("Ghost");

		foreach (GameObject go in ghosts){
			
			ge = go.GetComponent<GhostEvac>();

		}
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){

		if (col.CompareTag ("Ghost") == true) {

			SeesGhost = true;
			ge.inLineSight = false;

		} else {

			SeesGhost = false;
			ge.inLineSight = false;

		}


	}
}
