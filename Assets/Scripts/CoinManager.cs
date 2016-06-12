using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CoinManager : MonoBehaviour {

	public int pooledAmountCoins = 7;
	List<GameObject> coins;	
	List<GameObject> explosions;	

	public GameObject coin;
	public GameObject explosion;


	// Use this for initialization
	void Start () {

		coins = new List<GameObject> ();
		explosions = new List<GameObject> ();

		for(int i = 0; i < pooledAmountCoins; i++)
		{
			GameObject objCoins = (GameObject)Instantiate(coin);
			objCoins.SetActive(false);
			coins.Add(objCoins);

		}
		for(int i = 0; i < pooledAmountCoins; i++)
		{
			GameObject obj= (GameObject)Instantiate(explosion);
			obj.SetActive(false);
			explosions.Add(obj);
			
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InstantiateCoin(Vector3 position)
	{
		for (int i = 0; i < coins.Count; i++)
		{
			if(!coins[i].activeInHierarchy)
			{	
				Vector3 pos = position;
				pos.y = 1.25f;
				pos.z = -0.21f;
				coins[i].transform.position = pos;
				coins[i].transform.rotation = transform.rotation;

				coins[i].SetActive(true);
				coins[i].GetComponent<coinScript>().StartCoroutine("MoveUp");
				break;
			}
			
		}
	}

	public void InstantiateExplosion(Vector3 position)
	{
		for(int j = 0; j < explosions.Count; j++)
		{
			if(!explosions[j].activeInHierarchy)
			{
				explosions[j].transform.position = position;
				explosions[j].transform.rotation = transform.rotation;
				explosions[j].SetActive(true);
				explosions[j].GetComponent<ParticleSystem>().Clear();
				explosions[j].GetComponent<ParticleSystem>().Play();
				break;
			}
		}
	}
}
