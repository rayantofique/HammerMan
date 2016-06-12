using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinDataShow : MonoBehaviour {

	private GameObject dataPersistenceHandler;
	private int previousCoins;
	private int newCoins;
	public Text coinsStockpile;
	public Text coinsNew;
	public GameObject panelButtons;
	public GameObject stockpile;
	public GameObject newCoinShow;
	public GameObject buttonCoin;
	public Animator coinAnim;


	// Use this for initialization
	void Start () {
	
		Time.timeScale = 1;
		dataPersistenceHandler = GameObject.Find ("CoinDataPersistenceHandler");
		previousCoins = dataPersistenceHandler.GetComponent<CoinDatePersistence> ().previousCoins;
		newCoins = dataPersistenceHandler.GetComponent<CoinDatePersistence> ().givenCoins;

	}
	
	// Update is called once per frame			
	void Update () {
	


	}

	public void ClaimReward()
	{
		gameObject.GetComponent<AudioSource> ().Play ();
		coinsNew.text = "+" + newCoins.ToString();
		coinsStockpile.text = (previousCoins+newCoins).ToString();
		coinAnim.SetTrigger ("isClicked");
		panelButtons.SetActive (true);
		stockpile.SetActive (true);
		newCoinShow.SetActive (true);
		buttonCoin.SetActive (false);
	}
}
