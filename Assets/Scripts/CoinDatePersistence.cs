using UnityEngine;
using System.Collections;

public class CoinDatePersistence : MonoBehaviour {

	static CoinDatePersistence instance;

	public int previousCoins;
	public int givenCoins;

	public static CoinDatePersistence Instance
	{
		get { return instance;}
	}

	// Use this for initialization
	void Awake () {
	
		previousCoins = 0;
		givenCoins = 0;
		if (instance == null) 
		{
			instance = this;
		} 
		else if (instance != this) 
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame

}
