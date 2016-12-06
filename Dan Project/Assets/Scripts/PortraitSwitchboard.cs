using UnityEngine; 
using System.Collections;
using UnityEngine.UI;   

public class PortraitSwitchboard : MonoBehaviour {

public Image ellie;  
public Image poohpyangg;  
public Image saya;  
public Image zeus;  
FrandPortrait e;
FrandPortrait p; 
FrandPortrait s;
FrandPortrait z; 

	// Use this for initialization
	void Start () {

	e = ellie.GetComponent<FrandPortrait>();
	p = poohpyangg.GetComponent<FrandPortrait>();
	s = saya.GetComponent<FrandPortrait>();
	z = zeus.GetComponent<FrandPortrait>(); 
	
	}
	
	// Update is called once per frame
	void Update () { 
	
	}

	public void switchBoard (int sentNum)
	{
		if (sentNum == 1)
		{
			e.found = true;
		}

		if (sentNum == 2)
		{
			s.found = true;
		}

		if (sentNum == 3)
		{
			p.found = true;
		}

		if (sentNum == 4)
		{
			z.found = true;
		}
	}
}
