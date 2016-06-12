using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class StartScript : MonoBehaviour {

	public GameObject settingsPanel;
	public GameObject settingsPanelFinal;
	public GameObject tutorialDialog;
	public Toggle tutorialToggle;
	public Toggle vibrationToggle;
	private string isVibOn;

	public GameObject finalSettingsPanel;

	private bool isRunning = false;

	public Text timerText;

	public GameObject pausePanel;

	private float time;
	// Use this for initialization
	void Start () {

		//Advertisement.Initialize ("58045", true);
		int vibInt = PlayerPrefs.GetInt ("toggleVib");
		if (Application.loadedLevel == 0){
			if (vibInt == 0) {

				vibrationToggle.isOn = false;
			} else if (vibInt == 1) {
				vibrationToggle.isOn = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StartGame()
	{
		//StartCoroutine (ShowAdWhenReady ());

	}
	/*IEnumerator ShowAdWhenReady()
	{
		while (!Advertisement.isReady ()) {
			yield return null;
		}
		
		Advertisement.Show ();
	}*/

	//show settings menu
	public void SettingsMenu()
	{
		settingsPanel.SetActive (true);   
	}

	public void SettingsMenuScene()
	{
		settingsPanel.SetActive (true);   
	}
	public void SettingsMenuScene2()
	{
		settingsPanelFinal.SetActive (true);   
	}
	public void SettingsMenuExit2()
	{
		settingsPanelFinal.SetActive (false);
	}

	//exit settings
	public void SettingsMenuExit()
	{
		settingsPanel.SetActive (false);
	}


	//show tutorial menu
	public void TutMenu()
	{
		if (PlayerPrefs.GetString("NotShowTut") == "false") {
			tutorialDialog.SetActive (true);
		} 
		else {

			StartGameFinal();
		}
	}

	//start game
	public void StartGameFinal()
	{
		LoadingScript.show ();
		Application.LoadLevel (2);
	}
	//start tutorial
	public void StartTutorial()
	{
		LoadingScript.show ();
		Application.LoadLevel (1);
	}


	//go to main screen
	public void GoToMainMenu()
	{
		LoadingScript.show ();
		Application.LoadLevel (0);
	}
	public void GoToShop()
	{
		LoadingScript.show ();
		Application.LoadLevel (3);
	}

	public void VibrationToggle()
	{

		SaveVibrationSettings (vibrationToggle.isOn);

	}

	public void PauseGame()
	{
		Time.timeScale = 0;
		pausePanel.SetActive (true);
	}

	public void ResumeGame()
	{
		if (!isRunning) {
			StartCoroutine ("timer");
			isRunning = true;
		}
	}


	void SaveVibrationSettings(bool valueVibration)
	{
		if (valueVibration) {
			
			PlayerPrefs.SetInt ("toggleVib", 1);
		} else if (!valueVibration) {
			PlayerPrefs.SetInt ("toggleVib", 0);
		}

		PlayerPrefs.Save ();
		
	}

	IEnumerator timer()
	{
		timerText.text = "3";

		yield return StartCoroutine (WaitForRealSeconds(0.7f));

		timerText.text = "2";

		yield return StartCoroutine (WaitForRealSeconds(0.65f));

		timerText.text = "1";

		yield return StartCoroutine (WaitForRealSeconds(0.6f));

		Time.timeScale = 1;	

		isRunning = false;

		pausePanel.SetActive (false);
		timerText.text = "Paused";

	}

	IEnumerator WaitForRealSeconds (float waitTime) {
		float endTime = Time.realtimeSinceStartup+waitTime;
		
		while (Time.realtimeSinceStartup < endTime) {
			yield return null;
		}
	}
	
}
