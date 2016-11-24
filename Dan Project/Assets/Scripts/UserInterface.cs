using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour {
	public GameObject gameOverUI;
	public GameObject gameWinUI;
	// Use this for initialization
	void Start () {
		Scene scene = SceneManager.GetActiveScene();
		if (scene.name == "Test"){
			FindObjectOfType<Player>().OnDeath += GameOver;
			FindObjectOfType<Player>().OnWin += GameWin;
		}
	}

	// Update is called once per frame
	void GameOver () {
		gameOverUI.SetActive(true);
	}

	void GameWin(){
		gameWinUI.SetActive(true);
	}

	public void StartGame(){
		SceneManager.LoadScene("Test");
	}

	public void RestartGame(){
		SceneManager.LoadScene("Menu");
	}
}
