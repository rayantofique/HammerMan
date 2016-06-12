using UnityEngine;
using System.Collections;

public class JumpScript : MonoBehaviour {

	//private bool isJumping = false;
	public float jumpForce;
	public Transform jumpPoint;
	//private int mask;

	// Use this for initialization
	void Start () {

		//mask = 1 << LayerMask.NameToLayer ("Floor");
	
	}
	
	// Update is called once per frame
	void Update () {

		/*RaycastHit hit;
		if (isJumping) 
		{
			if (Physics.Raycast (jumpPoint.transform.position, new Vector3 (0, -1, 0), out hit, mask)) 
			{
				if(hit.distance <= 0.1)
				{
					isJumping = false;
					print ("K");
				}
				print ("true");
			}
		} 
		else if (!isJumping) 
		{
			if(Input.GetKeyDown("a"))
		   	{
				GetComponent<Rigidbody>().AddForce((Vector3.up * jumpForce), ForceMode.VelocityChange); 
				isJumping = true;
			}
		}*/
		//if (GetComponent<CharacterController> ().isGrounded) {
		//	print ("true");
		//}
	
	}
}
