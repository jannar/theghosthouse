using UnityEngine;
using System.Collections;

public class FrandCounter : MonoBehaviour {
public int numOfFrands;

	// Use this for initialization
	void Start () {

	numOfFrands = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

	Debug.Log("Frands: " + numOfFrands);
	
	}

	public void increaseFrands()
	{
		numOfFrands++;
	} 
}
