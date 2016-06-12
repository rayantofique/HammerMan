using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	public static int score;
	private static int coins;	
	public static int combo;

	public int highScore;
	public Text scoreBox;
	public Text scoreFinal;
	public Text comboFinal;
	public Text coinsFinal;
	public Text highScoreAlert;
	public Text highScoreBox;	
	private string highScoreKey = "HighScore";
	private string coinsKey = "Coins";

	public GameObject coinDataPersistence;
	private bool toggleGiftiz = false;


	// Use this for initialization
	void Start () {

		toggleGiftiz = false;
		//PlayerPrefs.SetInt (highScoreKey, 0);
		coinDataPersistence = GameObject.Find ("CoinDataPersistenceHandler");
		score = 0;
		coins = 0;
		highScore = PlayerPrefs.GetInt (highScoreKey);
		coins = PlayerPrefs.GetInt (coinsKey);

	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(scoreBox)
		{
			scoreBox.text = "Score: " + score.ToString();
			scoreFinal.text = score.ToString();
			combo = Mathf.Max (KillManagerScript.comboList.ToArray ());
			comboFinal.text = combo.ToString ();
			coinsFinal.text = coins.ToString ();
			highScoreBox.text = "High Score: " + highScore.ToString();

			if(score > highScore)
			{

				highScoreBox.text = "High Score: " + score.ToString();
				highScoreAlert.enabled = true;
				if(StarDeathScript.isDead)
				{
					PlayerPrefs.SetInt (highScoreKey, score);
					PlayerPrefs.Save ();
				}

			}
			else
			{
				highScoreAlert.enabled = false;
			}


		}

		if (score >= 150) 
		{
			if(!toggleGiftiz)
			{
				GiftizBinding.missionComplete();
				toggleGiftiz = true;

			}
		}


	}


	public void AddCoins()
	{
		coins += 1;
		PlayerPrefs.SetInt (coinsKey, coins);
		PlayerPrefs.Save ();
	}

	public void GiveCoins()
	{
		int giveCoins = 0;
		int previousCoins = coins;

		float probability = Random.Range (0, 101);
			
		if (probability <= 55) {
			giveCoins = 5;
		} else if (probability > 55 && probability <= 80) {
			giveCoins = 10;
		} else if (probability > 80 && probability <= 95) {
			giveCoins = 25;
		} else if (probability > 95) {
			giveCoins = 50;
		}
		coinDataPersistence.GetComponent<CoinDatePersistence> ().previousCoins = previousCoins;
		coinDataPersistence.GetComponent<CoinDatePersistence> ().givenCoins = giveCoins;
		coins += giveCoins;
		PlayerPrefs.SetInt (coinsKey, coins);
		PlayerPrefs.Save ();
		Application.LoadLevel ("coinScene");
		//that level will retrievedata from coin data persistence on awake
		//and then exexute the coroutine that shows the coin animation
	}
}
