using UnityEngine;
using System.Collections;

public class MessageToMachine : MonoBehaviour {

	void MessageToScript()
	{
		CentipedeBehaviour scriptCentipede;
		scriptCentipede = GetComponentInParent<CentipedeBehaviour> ();
		scriptCentipede.ExecuteBurrow ();
	}

}
