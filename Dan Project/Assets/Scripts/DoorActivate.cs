using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorActivate : MonoBehaviour {

	//scripts and objects
	public Canvas canvas;
	FrandCounter fc;
	Collider bc;
	//audio source stuff if time

	// Use this for initialization
	void Start () {

		//get collider
		bc = GetComponent<BoxCollider> ();

		//FrandCounter object
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		fc = canvas.GetComponentInChildren<FrandCounter> ();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (fc.numOfFrands >= 4) {
			Destroy (this.bc);
		}
	
	}
}
