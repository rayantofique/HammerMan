using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {



	public GameObject dialog;
	public GameObject buttonsPanel;
	public Button buttonMenu;
	public Button buttonRestart;
	public Button buttonShop;
	public GameObject adButtons;
	
	public void Hit()
	{

		StartCoroutine (WaitTime ());
		
	}

	IEnumerator WaitTime()
	{
		yield return new WaitForSeconds(2);
		dialog.SetActive (true);
		buttonsPanel.SetActive (true);
		buttonMenu.enabled = true;
		buttonRestart.enabled = true;
		buttonShop.enabled = true;
		if (CheckNet.internetOn) {

			adButtons.SetActive(true);
		}
	}

	
	
	// Update is called once per frame

}
