using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetHammer : MonoBehaviour {

	public List<Mesh> hammerMeshes;
	public List<Texture> hammerTextures;
	public Vector3[] HammerPositions;

	public MeshFilter player;
	public Renderer playerRenderer;


	// Use this for initialization
	void Awake () {



		for(int i = 0; i < hammerMeshes.Count; i++)
		{
			int isEquipped = PlayerPrefs.GetInt("IsEquipped" + i);
			if(isEquipped == 1)
			{
				player.mesh = hammerMeshes[i];
				playerRenderer.material.mainTexture = hammerTextures[i];
				player.transform.localPosition = HammerPositions[i];

				break;
			}
		}
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
