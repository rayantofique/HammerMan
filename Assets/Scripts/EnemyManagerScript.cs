using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManagerScript : MonoBehaviour {

	public GameObject enemySnail;
	public GameObject spider;
	public float spiderWaitTime = 8;
	public float enemyWaitTime = 1;
	public bool canCreateWorm;
	public float wormWaitTime = 5;
	public GameObject fireWorm;

	public int pooledAmount = 10;
	List<GameObject> snails;


	// Use this for initialization
	void Start () {

		snails = new List<GameObject> ();
		for (int i =0; i < pooledAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(enemySnail);
			obj.SetActive(false);
			snails.Add(obj);

		}


			StartCoroutine ("WormInstantiation");
			InvokeRepeating ("InstantiateSnail", 0, enemyWaitTime);


	
	}
	
	// Update is called once per frame
	void Update () {

		if (!StarDeathScript.isDead) {
			StartCoroutine ("SpiderInstantiation");
		}
	
	}

	void InstantiateSnail()
	{
		for (int i = 0; i < snails.Count; i++)
		{
			if(!snails[i].activeInHierarchy)
			{	
				RandomPos ();
				snails[i].transform.position = transform.position;
				snails[i].transform.rotation = transform.rotation;
				snails[i].SetActive(true);
				snails[i].GetComponent<EnemySnailBehaviour>().InitializeSnail();
				//snails[i].GetComponent<EnemySnailEvents>().InitializeSnailOther();
				break;
			}

		}
	}

	void RandomPos()
	{
		float posx = Random.Range (-11, 11);
		Vector3 pos = transform.position;
		pos.x = posx;
		transform.position = pos;
	}

	public IEnumerator WormInstantiation()
	{
		if (canCreateWorm) {
			yield return new WaitForSeconds (wormWaitTime);	
			GameObject obj = Instantiate (fireWorm, new Vector3 (-7.91f, -9.14f, -1.54f), Quaternion.identity) as GameObject;
			//obj.GetComponent<CentipedeBehaviour>().minDistance = 5;

		}
		
	}
	public IEnumerator SpiderInstantiation()
	{
		if (canCreateWorm) {
			yield return new WaitForSeconds (spiderWaitTime);
			Instantiate (spider, new Vector3 (-12.91f, -3.63f, -1.04f), Quaternion.Euler (0, 210, 0));
			StopCoroutine ("SpiderInstantiation");
		}

	}

}
