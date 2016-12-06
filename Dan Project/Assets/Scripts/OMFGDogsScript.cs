using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OMFGDogsScript : MonoBehaviour {

	//!!!!!!!THIS IS ON THE CORGI!!!!!!!!!

	//stuff to grab
	FrandCounter fc;
	AudioSource dogSong;
	MusicManager mm;
	public Canvas canvas;
	GameObject audioManager;

	//public things
	public AudioClip omfgdogs;
	public bool allFriendsFound = false;
	public bool hasPlayed = false;

	// Use this for initialization
	void Start () {

		//AUDIO SOURCE ON CORGI
		dogSong = this.GetComponent<AudioSource> ();

		//FrandCounter object
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		fc = canvas.GetComponentInChildren<FrandCounter> ();

		//Music Manager script
		audioManager = GameObject.Find("Music Man");
		mm = audioManager.GetComponent<MusicManager>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (fc.numOfFrands >= 4) {
			//change the bool just to be nice
			allFriendsFound = true;

			//stop playing main theme
			if (mm.source.isPlaying){
				mm.source.Stop ();
			}

			//start playing OMFG DOGS
			if (hasPlayed == false) {
				dogSong.PlayOneShot (omfgdogs);
				hasPlayed = true;
			}
		}
	}
}
