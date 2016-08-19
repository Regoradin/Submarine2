using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {

	public float waviness;
	private Vector3 difference;
	public float waterDensity;


	// Use this for initialization
	void OnTriggerStay (Collider other) {
		float volume = other.bounds.size.x * other.bounds.size.y * other.bounds.size.z;

		if (other.attachedRigidbody)
			other.attachedRigidbody.AddForce (volume * -Physics.gravity * waterDensity);
	
	}
	
	// Update is called once per frame
	void Update () {

		difference = (Mathf.Sin (Mathf.Repeat (Time.time, 2 * Mathf.PI) * waviness) - transform.position.y) * Vector3.up;

		transform.Translate (difference);
	}
}
