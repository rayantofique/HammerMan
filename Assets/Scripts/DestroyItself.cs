using UnityEngine;
using System.Collections;

public class DestroyItself : MonoBehaviour {

	public Transform particles;
	public int id;
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y < -4) {
			Destroy(this.gameObject);
		}
		if (id == 1) {

			StartCoroutine("DestroyEffect");
		}
		if (id == 2) {
			
			StartCoroutine("DestroyEffect2");
		}
	
	}

	void OnCollisionEnter(Collision col)
	{
		Destroy (this.gameObject);
		Instantiate (particles, transform.position, Quaternion.identity);	

	}
	IEnumerator DestroyEffect()
	{
		yield return new WaitForSeconds (1);
		Destroy (this.gameObject);
	}
	IEnumerator DestroyEffect2()
	{
		yield return new WaitForSeconds (3);
		Destroy (this.gameObject);
	}

}
