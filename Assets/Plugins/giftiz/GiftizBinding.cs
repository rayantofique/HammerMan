using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_ANDROID
public class GiftizBinding
{
	public enum GiftizButtonState {
		Invisible = 0,
		Naked	  = 1,
		Badge	  = 2,
		Warning	  = 3
	}

	public static GiftizButtonState giftizButtonState = GiftizButtonState.Invisible;
	
	private static AndroidJavaObject _GiftizPlugin;

	static GiftizBinding()
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_GiftizPlugin = new AndroidJavaObject("com.purplebrain.giftiz.sdk.GiftizUnityBinding");
	}

	// Called on pause (MonoBehaviour.OnEnable)
	public static void onResume()
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_GiftizPlugin.Call("onResume");
	}

	
	// Called on pause (MonoBehaviour.OnDisable)
	public static void onPause()
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_GiftizPlugin.Call("onPause");
	}

	
	// Sets the Giftiz mission as complete
	public static void missionComplete()
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_GiftizPlugin.Call("missionComplete");
	}
	
	// Registers an in-app purchase
	public static void inAppPurchase(float amountPayedByUser)
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_GiftizPlugin.Call("inAppPurchase", amountPayedByUser);
	}
	
	// Asks for the Giftiz button status
	public static void getButtonStatus()
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_GiftizPlugin.Call("getButtonStatus");
	}
	
	// Click on the Giftiz button
	public static void buttonClicked()
	{
		if (Application.platform != RuntimePlatform.Android)
			return;

		_GiftizPlugin.Call("buttonClicked");
	}
}
#endif
