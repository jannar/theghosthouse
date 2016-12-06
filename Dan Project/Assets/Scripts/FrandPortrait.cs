using UnityEngine;
using System.Collections;

public class FrandPortrait : MonoBehaviour {

public bool found;

	// Use this for initialization
	void Start () {

	found = false;
	
	}
	
	// Update is called once per frame
	void Update () {

	animationHandler();
	
	}


	void animationHandler ()
	{
		GetComponent<Animator>().SetBool("Found", found);
	}

	public void frandFound()
	{
		found = true;
	}
}
