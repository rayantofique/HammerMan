using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour {


	static LoadingScript instance;
	public GameObject loadingScreenImage;
	public GameObject headerImage;
	public Text loadingText;
	public Text tipsLabel;
	public Text tips;
	public string[] tipsText;

	void Awake()
	{
		if (instance) {
			Destroy(gameObject);
			hide();
			return;
		}
		instance = this;
		instance.loadingScreenImage.SetActive (false);
		instance.headerImage.SetActive (false);
		instance.loadingText.gameObject.SetActive (false);
		instance.tipsLabel.gameObject.SetActive (false);
		instance.tips.gameObject.SetActive (false);
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

		if(!Application.isLoadingLevel)
		{
			hide();
		}
	
	}

	public static void show()
	{
		//if instance does not exists return from this function
		if (!InstanceExists()) 
		{
			return;
		}



		//enable the loading image object 
		instance.loadingScreenImage.SetActive(true);
		instance.loadingText.gameObject.SetActive (true);
		instance.tipsLabel.gameObject.SetActive (true);
		instance.headerImage.SetActive (true);
		int rand = Random.Range (0, 14);
		string tip = instance.tipsText [rand];
		instance.tips.gameObject.SetActive (true);
		instance.tips.text = tip;
	}

	public static void hide()
	{
		if (!InstanceExists()) 
		{
			return;
		}
		instance.loadingScreenImage.SetActive(false);
		instance.headerImage.SetActive (false);
		instance.loadingText.gameObject.SetActive (false);
		instance.tipsLabel.gameObject.SetActive (false);
		instance.tips.gameObject.SetActive (false);
	}

	static bool InstanceExists()
	{
		if (!instance)
		{
			return false;
		}
		return true;
		
	}
}
