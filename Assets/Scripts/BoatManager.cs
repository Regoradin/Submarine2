using UnityEngine;
using System.Collections;

public class BoatManager : MonoBehaviour {

	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();

		rigidBody.centerOfMass = new Vector3 (0, -10, 0);
		Debug.Log ("Center of Mass: " + rigidBody.centerOfMass);

	}

	void Update(){

		//Debug.Log (rigidBody.velocity.magnitude);

	}
}
