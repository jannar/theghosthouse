using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EscapeButton : MonoBehaviour {

public Color alphadOut;
public Color alphadIn; 
GameObject frandCounter;
FrandCounter fc;
Image img; 


	// Use this for initialization
	void Start () {

	img = GetComponent<Image>();
	frandCounter = GameObject.Find("FrandCounter");
	fc = frandCounter.GetComponent<FrandCounter>();
	
	}
	
	// Update is called once per frame
	void Update () {

	if (fc.numOfFrands < 4)
	{
		img.color = alphadOut;
	}

	if (fc.numOfFrands >=4)
	{
		img.color = alphadIn;
	}
	
	}
}
