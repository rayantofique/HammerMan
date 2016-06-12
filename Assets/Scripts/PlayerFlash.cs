using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFlash : MonoBehaviour {

	public List<MeshRenderer> bodyParts;
	public List<Material> bodyPartsMaterials;
	public Material flashMaterial;
	public float flashSeconds;

 	IEnumerator FlashPlayer()
	{
		for (int i = 0; i < bodyParts.Count; i++) {

			bodyParts[i].material = flashMaterial;

		}
		yield return new WaitForSeconds (flashSeconds);

		for (int i = 0; i < bodyParts.Count; i++) {

			if(i == 0)
			{
				bodyParts[i].material = bodyPartsMaterials[0];
			}
			else
			{
				bodyParts[i].material = bodyPartsMaterials[1];
			}
						
		}

		StopCoroutine ("FlashPlayer");
	}


}
