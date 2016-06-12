using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GiftizController : MonoBehaviour {

	public Sprite Naked;
	public Sprite Normal;
	public Sprite Gifted;
	public Sprite Warning;
	public Button giftizButton;

	// Use this for initialization
	void Start () {
	
	}

	public void ChangeGiftizButtonSprite()
	{
		Image sp = giftizButton.image;
	
		switch (GiftizBinding.giftizButtonState) { 

		case GiftizBinding.GiftizButtonState.Invisible : 
			sp.sprite = Naked; 
			break; 
		case GiftizBinding.GiftizButtonState.Naked : 
			sp.sprite = Naked; 
			break; 
		case GiftizBinding.GiftizButtonState.Badge : 
			sp.sprite = Gifted; 
			break; 
		case GiftizBinding.GiftizButtonState.Warning : 
			sp.sprite = Warning; 
			break; 

		} 
	}

	public void GiftizButtonClicked()
	{
		GiftizBinding.buttonClicked ();
	}

	// Update is called once per frame
	void Update () {
	
		ChangeGiftizButtonSprite ();


	}
}
