using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	public float climbSpeed;

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			//goes all figity because player mover is also calling move() with a downward move, so it goes weird. Fix later.
			other.GetComponent<CharacterController>().Move(Vector3.up * climbSpeed);
		}
	}
}