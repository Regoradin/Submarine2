using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {

	public float waviness;
	private Vector3 difference;
	public float waterDensity;
	public float waterDrag;
	private float oldDrag;


	// Use this for initialization
	void OnTriggerStay (Collider other) {
		float volume = other.bounds.size.x * other.bounds.size.y * other.bounds.size.z;

		float waterlevel = transform.position.y + (transform.localScale.y / 2);
		volume = volume * (other.bounds.extents.y / waterlevel);
		//Debug.Log (other.bounds.extents);
		//Debug.Log ("waterlevel: " + waterlevel);

		if (other.attachedRigidbody)
			other.attachedRigidbody.AddForce (volume * -Physics.gravity * waterDensity);
	
	}

	void OnTriggerEnter (Collider other){

		if (other.attachedRigidbody) {
			oldDrag = other.attachedRigidbody.drag;
			other.attachedRigidbody.drag = waterDrag;
		}

	}

	void OnTriggerExit (Collider other){

		if (other.attachedRigidbody)
			other.attachedRigidbody.drag = oldDrag;

	}

	// Update is called once per frame
	void Update () {

		difference = (Mathf.Sin (Mathf.Repeat (Time.time, 2 * Mathf.PI) * waviness) - transform.position.y) * Vector3.up;

		transform.Translate (difference);
	}
}
