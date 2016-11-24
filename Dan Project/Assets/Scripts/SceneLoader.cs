using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	public GameObject player;
	public bool start = false;
	public bool game = false;

//	void awake(){
//		DontDestroyOnLoad(transform.gameObject);
//	}

	// Use this for initialization
	void Start () {
		Scene scene = SceneManager.GetActiveScene();
		if (scene.name == "Menu"){
			start = true;
			game = false;
		}
		if (scene.name == "Test"){
			start = false;
			game = true;
		}
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(start == true && game == false){
			if(Input.GetKeyDown(KeyCode.Space)){
				SceneManager.LoadScene("Test");
			}	
		}
		if(start == false && game == true)
		{
			if(Input.GetKeyDown(KeyCode.R)){
				SceneManager.LoadScene("Menu");
			}
		}
	}
}
