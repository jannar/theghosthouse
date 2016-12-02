using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PortraitChange : MonoBehaviour {

public Image content; 
public bool happy; 
public bool wow;
public bool kindaSpoop;
public bool verySpoop;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	howSpooped();
	animationHandler();
	
	}

	public void howSpooped()
	{
		if (content.fillAmount <= 0f) 
		{
			happy = true; 
			wow = false;
			kindaSpoop = false;
			verySpoop = false;

		}

		if (content.fillAmount > 0f && content.fillAmount < 0.33f)
		{
			happy = false;
			wow = true;
			kindaSpoop = false;
			verySpoop = false;

		}

		if (content.fillAmount > 0.33f && content.fillAmount < 0.66f)
		{
			happy = false;
			wow = false;
			kindaSpoop = true;
			verySpoop = false;
		}

		if (content.fillAmount > 0.66f)
		{
			happy = false; 
			wow = false;
			kindaSpoop = false;
			verySpoop = true;
		}


	}

	void animationHandler ()
	{
		GetComponent<Animator>().SetBool("Happy", happy);
		GetComponent<Animator>().SetBool("Wow", wow);
		GetComponent<Animator>().SetBool("Kinda Spooped",kindaSpoop);
		GetComponent<Animator>().SetBool("Very Spooped", verySpoop);
	}
}
