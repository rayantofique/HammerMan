	using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CameraShop : MonoBehaviour {



	private List<Hammers> hammerList;
	private List<string> hammerSold;
	
	Hammers hammer1;
	Hammers hammer2;
	Hammers hammer3;
	Hammers hammer4;
	Hammers hammer5;
	Hammers hammer6;

	public Text hammerLabel;
	public Text sellButtonText;
	public Text hammerPriceText;
	public Text coinCounter;
	public Text notification;

	public AudioClip buyingSound;
	public AudioClip equippingSound;

	private int index = 0;

	private bool isLerping;		
	private Vector3 startPos;
	private Vector3 endPos;
	private float timeStartedLerping;
	public float timeTakenDuringLerp = 1f;
	// Use this for initialization
	void Start () {



		Time.timeScale = 1;
		/*PlayerPrefs.DeleteKey ("IsSold1");
		PlayerPrefs.DeleteKey ("IsSold2");
		PlayerPrefs.DeleteKey ("IsSold3");
		PlayerPrefs.DeleteKey ("IsSold4");

		PlayerPrefs.DeleteKey ("IsEquipped1");
		PlayerPrefs.DeleteKey ("IsEquipped2");
		PlayerPrefs.DeleteKey ("IsEquipped3");
		PlayerPrefs.DeleteKey ("IsEquipped4");*/

		hammerList = new List<Hammers> ();
		hammerSold = new List<string> ();


		hammer1 = gameObject.AddComponent<Hammers> ();
		hammer2 = gameObject.AddComponent<Hammers> ();
		hammer3 = gameObject.AddComponent<Hammers> ();
		hammer4 = gameObject.AddComponent<Hammers> ();
		hammer5 = gameObject.AddComponent<Hammers> ();
		hammer6 = gameObject.AddComponent<Hammers> ();

	

		InitializeHammers();


		if (hammer1.isEquipped == 1) {
			sellButtonText.text = "Equipped";

		} else {
			sellButtonText.text = "Equip";
		}

	}
	
	// Update is called once per frame
	void Update () {

		coinCounter.text = PlayerPrefs.GetInt("Coins").ToString();

		if (Input.GetKeyDown("a")) {

			if(index == (hammerList.Count-1))
			{


			}
			else{

				index +=1;
			}

			switchHammer(index);

		}
		if (Input.GetKeyDown ("d")) 
		{
			if(index == 0)
			{

			}
			else
			{
				index -=1;
			}

			switchHammer(index);
		}


		if (isLerping) 
		{

			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / timeTakenDuringLerp;
			transform.position = Vector3.Lerp (startPos, endPos, percentageComplete);

			if(percentageComplete >= 1.0f)
			{
				isLerping = false;
			}
		}

	}

	public void isButtonRightPressed()
	{
		if(index == (hammerList.Count-1))
		{

			
		}
		else{
			
			index +=1;
		}
		switchHammer(index);
	}

	public void isButtonLeftPressed()
	{
		if(index == 0)
		{

		}
		else
		{
			index -=1;
		}
		
		switchHammer(index);
		
	}


	public void InitializeHammers()
	{

		hammer1.SetHammer (1, "Hammer", "Free", new Vector3(0,0.12f,-3.27f));
		hammer2.SetHammer (2, "Mallet", "50", new Vector3(8.04f, 0.12f , -3.27f));
		hammer3.SetHammer (3, "Morning Star", "250", new Vector3(14.6f, 0.12f, -3.27f));
		hammer4.SetHammer (4, "Basher", "500", new Vector3(22.67f, 0.12f, -3.27f));
		hammer5.SetHammer (5, "Meat Hammer", "800", new Vector3 (30.4f, 0.12f, -3.27f));
		hammer6.SetHammer (6, "Homo-Hammer", "1000", new Vector3 (37.7f, 0.12f, -3.27f));

		hammer1.isHammerSold(PlayerPrefs.GetInt("IsSold0", 1));
		hammer2.isHammerSold(PlayerPrefs.GetInt("IsSold1", 0));
		hammer3.isHammerSold(PlayerPrefs.GetInt("IsSold2", 0));
		hammer4.isHammerSold(PlayerPrefs.GetInt("IsSold3", 0));
		hammer5.isHammerSold(PlayerPrefs.GetInt("IsSold4", 0));
		hammer6.isHammerSold(PlayerPrefs.GetInt("IsSold5", 0));

		hammer1.isHammerEquipped(PlayerPrefs.GetInt("IsEquipped0", 1));
		hammer2.isHammerEquipped(PlayerPrefs.GetInt("IsEquipped1", 0));
		hammer3.isHammerEquipped(PlayerPrefs.GetInt("IsEquipped2", 0));
		hammer4.isHammerEquipped(PlayerPrefs.GetInt("IsEquipped3", 0));
		hammer5.isHammerEquipped(PlayerPrefs.GetInt("IsEquipped4", 0));
		hammer6.isHammerEquipped(PlayerPrefs.GetInt("IsEquipped5", 0));


	

		hammerList.Add (hammer1);
		hammerList.Add (hammer2);	
		hammerList.Add (hammer3);
		hammerList.Add (hammer4);	
		hammerList.Add (hammer5);
		hammerList.Add (hammer6);	
	

	}

	public void switchHammer(int i)
	{
		StartLerping(hammerList [i].position);
		notification.enabled = false;
		hammerLabel.text = hammerList [i].HammerName;
		hammerPriceText.text = hammerList [i].price;
		if (hammerList [i].isSold == 1) {

			sellButtonText.text = "Equip";

		}
		else if(hammerList[i].isSold == 0)
		{
			sellButtonText.text = "Buy";

		}

		if (hammerList [i].isEquipped == 1) {

			sellButtonText.text = "Equipped";

		}
	}
	

	void StartLerping(Vector3 pos)
	{
		isLerping = true;
		timeStartedLerping = Time.time;

		startPos = transform.position;
		endPos = pos;
	}

	public void saveHammer()
	{
		if(hammerList[index].isSold == 0)
		{

			int coins = PlayerPrefs.GetInt("Coins");
			int priceHammer = int.Parse(hammerList[index].price);
			if(coins >= priceHammer)
			{
				AudioSource.PlayClipAtPoint(buyingSound, transform.position);
				coins -= priceHammer;
				PlayerPrefs.SetInt("Coins", coins);
				PlayerPrefs.SetInt(("IsSold" + index),1);	
				hammerList[index].isHammerSold(1);
				sellButtonText.text = "Equip";

			}
			else {


				notification.enabled = true;
			}

		}
		else if (hammerList [index].isSold == 1) 
		{
			//when clicked, below should happen
			AudioSource.PlayClipAtPoint(equippingSound, transform.position);
			sellButtonText.text = "Equipped";
			hammerList[index].isHammerEquipped(1);
			PlayerPrefs.SetInt("IsEquipped" + index, 1);
			for (int i = 0; i < hammerList.Count; i++)
			{
				if(i != index)
				{
					hammerList[i].isHammerEquipped(0);
					PlayerPrefs.SetInt("IsEquipped" + i, 0);
				}
			}

		}

	}





}
