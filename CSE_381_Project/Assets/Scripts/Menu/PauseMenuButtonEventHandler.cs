using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuButtonEventHandler : MonoBehaviour {

    public GameObject pauseScreenBackground;
    public GameObject pauseScreen;
    public GameObject helpScreen;
    private GameObject currentScreen;
    public Text levelNameText;
    public void Start() {
        currentScreen = pauseScreen;
        if (SceneManager.GetActiveScene().name.Equals("Level0")){
            levelNameText.text = "Tutorial Level";
        }
        if (SceneManager.GetActiveScene().name.Equals("Level1"))
        {
            levelNameText.text = "Level 1: Understanding Obelisks";
        }
    }

    public void ClosePauseScreen()
    {
        GameManager.instance.unpauseLogic();
        currentScreen = null;
        pauseScreen.SetActive(false);
        helpScreen.SetActive(false);
        pauseScreenBackground.SetActive(false);
        
    }

    public void LoadPauseScreen(){
        if (currentScreen) { 
		    currentScreen.SetActive (false);
        }
        pauseScreen.SetActive (true);
		currentScreen = pauseScreen;
        pauseScreenBackground.SetActive(true);
    }

	public void LoadMainScreen(){
		SceneManager.LoadScene ("_scenes/Menu");
	}

	public void LoadHelpScreen(){
        if (currentScreen) { 
		    currentScreen.SetActive (false);
        }
        helpScreen.SetActive (true);
		currentScreen = helpScreen;
	}

    public void RestartLevel() {
        GameManager.instance.restartLevel();
    }

	public void Exit(){
        Application.Quit();
	}
}
