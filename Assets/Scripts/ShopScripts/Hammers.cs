using UnityEngine;
using System.Collections;

public class Hammers : MonoBehaviour {


	public int id;



	public string HammerName;
	public string price;
	public Vector3 position;
	public int isSold;
	public int isEquipped;

	public void SetHammer(int Id, string Name, string Price, Vector3 Position)
	{
		id = Id;
		HammerName = Name;
		price = Price;
		position = Position;

	}
	public void isHammerSold(int IsSold)
	{
		isSold = IsSold;
	}

	public void isHammerEquipped(int IsEquipped)
	{
		isEquipped = IsEquipped;
	}


}
