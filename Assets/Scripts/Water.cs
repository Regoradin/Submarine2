using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	public float waveFrequency;
	public float waveHeight;
	public float waveSize;
	private Vector3 difference;

	public float waterDrag;
	private float oldDrag;

	void OnTriggerEnter (Collider other){
		if (other.tag == "Floatable") {
			if (other.attachedRigidbody) {
				oldDrag = other.attachedRigidbody.drag;
				other.attachedRigidbody.drag = waterDrag;
			}
		}

	}

	void OnTriggerExit (Collider other){
		if (other.tag == "Floatable") {
			if (other.attachedRigidbody)
				other.attachedRigidbody.drag = oldDrag;
		}

	}

	void Update () {

		//+(transform.position.z/10) is a placeholder to offset the wave timing until some procedural wave generator is created
		difference = (Mathf.Sin (Mathf.Repeat (Time.time + (transform.localPosition.z/waveSize), 2 * Mathf.PI ) * waveFrequency) * waveHeight - transform.position.y) * Vector3.up ;


		transform.Translate (difference);
	}
}