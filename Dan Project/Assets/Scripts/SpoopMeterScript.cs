using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpoopMeterScript : MonoBehaviour {


public float maxSpoopLevel; 
public float currentSpoopLevel; 
public float spoopIncreaseRate; 
public float spoopRateModifier;


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


}
