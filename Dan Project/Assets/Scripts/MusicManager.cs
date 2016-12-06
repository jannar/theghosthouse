using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	//changed some of Dan's code to get the music to play or not play on a button press.
	//probably not necessary, but I wanted to see how it worked. *shrug*

	public AudioClip mainTheme;
	public AudioClip menuTheme;
	private AudioSource source;

	void Start () {
		AudioManager.instance.PlayMusic(menuTheme,2);
		source = GetComponent<AudioSource> ();
		source.clip = mainTheme;
	}

	// Update is called once per frame
	void Update () {
//		if(Input.GetKeyDown(KeyCode.Space)){
//			AudioManager.instance.PlayMusic(mainTheme,3);
//		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (source.isPlaying) {
				source.Stop ();
			} else {
				source.Play ();
			}
		}
	
	}
}
