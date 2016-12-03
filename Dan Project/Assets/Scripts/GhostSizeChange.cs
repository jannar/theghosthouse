using UnityEngine;
using System.Collections;

public class GhostSizeChange : MonoBehaviour {

public float minSize;
public float maxSize;
private float sizeNumber;

	// Use this for initialization
	void Start () 
	{
		sizeNumber = Random.Range (minSize, maxSize);
		transform.localScale = new Vector3 (sizeNumber, sizeNumber, 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
