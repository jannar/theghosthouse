using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LerpingColors : MonoBehaviour {

public Color bright;
public Color dark; 
public Color lerpingColor;
public float lerpSpeed;
Image img; 

	// Use this for initialization
	void Start () {

		img = GetComponent<Image>();
	
	}
	
	// Update is called once per frame
	void Update () {
	lerpingColor = Color.Lerp(bright, dark, Mathf.PingPong(Time.time*lerpSpeed, 1));
	img.color = lerpingColor;
	
	}
}
