using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {


	float volumeMaster = .2f; //master volume percent
	float volumeSFX = 1f; // sfx volume percent
	float volumeMusic = 1f; //music volume percent

	AudioSource[] musicSources; //array of audio sources
	int activeMusicSourceIndex; 

	public static AudioManager instance;

	Transform audioListenerTransform; //audio listener transform
	Transform playerTransform; //player transform


	void Awake(){

		if(instance != null){
			Destroy(gameObject);
		}else{
			instance = this;
			DontDestroyOnLoad(gameObject);


			musicSources = new AudioSource[2];
			for(int i = 0; i < 2; i++){
				GameObject newMusicSource = new GameObject("Music source " + (i + 1));
				musicSources[i] = newMusicSource.AddComponent<AudioSource>();
				newMusicSource.transform.parent = transform;
			}

			audioListenerTransform = FindObjectOfType<AudioListener>().transform; //gets the transform of our audiolistener
			playerTransform = FindObjectOfType<Player>().transform; // getst the transform of our player

		}
	}

	void Update(){
		if (playerTransform != null){
			audioListenerTransform.position = playerTransform.position;
		}
	}



	public void PlayMusic(AudioClip clip, float fadeDuration = 1){
		activeMusicSourceIndex = 1 - activeMusicSourceIndex;
		musicSources[activeMusicSourceIndex].clip = clip;
		musicSources[activeMusicSourceIndex].Play();

		StartCoroutine(AnimateMusicCrossfade(fadeDuration));
	}
		
	public void PlaySound(AudioClip clip, Vector3 position) { //method that takes an audio clip and position
		if(clip != null){ //if there is a clip to play
			AudioSource.PlayClipAtPoint(clip, position, volumeSFX * volumeMaster); //plays clip, at position, at set volume
		}
	}
		

	IEnumerator AnimateMusicCrossfade(float duration){
		float percent = 0;

		while(percent < 1){
			percent += Time.deltaTime * 1/duration;
			musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, volumeMusic * volumeMaster, percent);
			musicSources[1-activeMusicSourceIndex].volume = Mathf.Lerp(volumeMusic * volumeMaster, 0, percent);
			yield return null;
		}
	}
}
