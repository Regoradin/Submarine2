using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {

	public float waviness;
	private Vector3 difference;

	public float waterDrag;
	private float oldDrag;

	public float shipWaviness;
	public float waterDensity;


	// Use this for initialization
	void OnTriggerStay (Collider other) {
		if (!other.isTrigger && other.tag == "Boat") {
			float bottomSurfaceArea = other.bounds.size.x * other.bounds.size.y;
			float waterLevel = transform.position.y + GetComponent<Collider> ().bounds.extents.y;
		
			float optimalDepth = ((-other.attachedRigidbody.mass) / (waterDensity * bottomSurfaceArea) + waterLevel);

			other.transform.position = new Vector3(other.transform.position.x, Mathf.Lerp (other.transform.position.y, optimalDepth, shipWaviness), other.transform.position.y);

			//Debug.Log ("waterLevel: " + waterLevel);
			//Debug.Log ("OptimalDepth: " + optimalDepth);
			//Debug.Log ("Lerp: " + Mathf.Lerp (other.transform.position.y, optimalDepth, shipWaviness));

			//if (other.attachedRigidbody)
			//	other.attachedRigidbody.AddForce (volume * -Physics.gravity * waterDensity);
			//Debug.Log (volume * -Physics.gravity * waterDensity);
		}
	
	}

	void OnTriggerEnter (Collider other){
		if (other.tag == "Boat") {
			if (other.attachedRigidbody) {
				oldDrag = other.attachedRigidbody.drag;
				other.attachedRigidbody.drag = waterDrag;
			}
		}

	}

	void OnTriggerExit (Collider other){
		if (other.tag == "Boat") {
			if (other.attachedRigidbody)
				other.attachedRigidbody.drag = oldDrag;
		}

	}

	// Update is called once per frame
	void Update () {

		difference = (Mathf.Sin (Mathf.Repeat (Time.time, 2 * Mathf.PI) * waviness) - transform.position.y) * Vector3.up;

		transform.Translate (difference);
	}
}
