using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuButtonEventHandler : MonoBehaviour {

	public GameObject pauseScreen;
	public GameObject helpScreen;
	private GameObject currentScreen;


	public void LoadPauseScreen(){
		currentScreen.SetActive (false);
		pauseScreen.SetActive (true);
		currentScreen = pauseScreen;
	}

	public void LoadMainScreen(){
		SceneManager.LoadScene ("_scenes/Menu");
	}

	public void LoadHelpScreen(){
		currentScreen.SetActive (false);
		helpScreen.SetActive (true);
		currentScreen = helpScreen;
	}

	public void Exit(){
		
	}
}
