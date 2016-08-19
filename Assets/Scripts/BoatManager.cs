using UnityEngine;
using System.Collections;

public class BoatManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Rigidbody rigidBody = GetComponent<Rigidbody> ();


		//rigidBody.centerOfMass = Vector3.zero;
		//Debug.Log (rigidBody.centerOfMass);

	}

	void Update(){

		Debug.Log (GetComponent<Rigidbody>().angularVelocity);

	}
}
