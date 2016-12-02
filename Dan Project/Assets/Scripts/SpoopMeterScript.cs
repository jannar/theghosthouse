using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpoopMeterScript : MonoBehaviour {


public float maxSpoopLevel; 
public float currentSpoopLevel; 
public float spoopIncreaseRate; 
public float spoopRateModifier;
public bool happy; 
public bool wow;
public bool kindaSpoop;
public bool verySpoop;


	


public Image content; 

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

	if (currentSpoopLevel < 0) 
	{
		currentSpoopLevel = 0;
	}

	handleBar (); 
	howSpooped (); 
	
	}

	void handleBar ()
	{
		content.fillAmount = Map(currentSpoopLevel, maxSpoopLevel); 
	}

	private float Map (float currentValue, float maxValue) 
	{

		return currentValue / maxValue;

	}

	public void increaseMeter(int numberOfGhosts)
	{
	spoopIncreaseRate = numberOfGhosts * spoopRateModifier;
		if (numberOfGhosts >=1) 
		{
			currentSpoopLevel = currentSpoopLevel + spoopIncreaseRate;
		}
	}

	public void howSpooped()
	{
		if (content.fillAmount <= 0f) 
		{
			happy = true; 
			wow = true;
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
}
