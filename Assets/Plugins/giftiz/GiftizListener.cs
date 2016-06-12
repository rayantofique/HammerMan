using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_ANDROID
public class GiftizListener : MonoBehaviour
{
	public void OnEnable()
	{
		GiftizBinding.onResume();
	}

	public void Start()
	{
		GiftizBinding.getButtonStatus();
	}

	public void OnApplicationPause(bool pause)
	{
		if (pause) {
			GiftizBinding.onPause();
		} else {
			GiftizBinding.onResume();
		}
	}

	// Received if the button needs an update
	public void buttonNeedsUpdate(string buttonStatus)
	{
		switch (buttonStatus) {
			case "ButtonInvisible":
				GiftizBinding.giftizButtonState = GiftizBinding.GiftizButtonState.Invisible;
				break;
			case "ButtonNaked":
				GiftizBinding.giftizButtonState = GiftizBinding.GiftizButtonState.Naked;
				break;
			case "ButtonBadge":
				GiftizBinding.giftizButtonState = GiftizBinding.GiftizButtonState.Badge;
				break;
			case "ButtonWarning":
				GiftizBinding.giftizButtonState = GiftizBinding.GiftizButtonState.Warning;
				break;
		}
	}
}
#endif
