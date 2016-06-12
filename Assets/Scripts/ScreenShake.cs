using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {

	public float shakeAmount;
	public float shakeDuration;
	private float timeShaking;
	private Vector3 originalPos;
	private float shakeSlow;
	private Vector3 newPos;
	private float originalShake;


	private float yPos;

	// Use this for initialization
	void Start () {

		originalShake = shakeAmount;
		timeShaking = 99;
		originalPos = transform.localPosition;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (timeShaking <= shakeDuration) 
		{
			newPos.x = Random.Range (-shakeAmount, shakeAmount);

			newPos.y = Random.Range (-shakeAmount, shakeAmount);
			newPos.z = originalPos.z;
			transform.localPosition= newPos;
			timeShaking += Time.deltaTime;
			shakeAmount -= shakeSlow;
		
		} 
		else 
		{

			shakeAmount = originalShake;
			transform.localPosition = originalPos;
			timeShaking = 99;
		}
	
	}

	public void CameraShake(float shakeValue, float shakeDur)
	{
		timeShaking = 0f;
		shakeAmount = shakeValue;
		shakeDuration = shakeDur;
		shakeSlow = shakeAmount / (shakeDuration / Time.deltaTime);
	}
}
