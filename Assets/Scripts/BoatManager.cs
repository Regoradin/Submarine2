using UnityEngine;
using System.Collections;

public class BoatManager : MonoBehaviour {

	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();

		Debug.Log ("Center of Mass: " + rigidBody.centerOfMass);

	}

	void Update(){

		//Debug.Log (rigidBody.velocity.magnitude);

	}
}
