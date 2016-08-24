using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {

	public float waveFrequency;
	public float waveHeight;
	public float waveSize;
	private Vector3 difference;

	public float waterDrag;
	private float oldDrag;

	public float shipWaviness;
	public float waterDensity;


	// Use this for initialization
	void OnTriggerStay (Collider other) {
		if (!other.isTrigger && other.tag == "Floatable") {
			float bottomSurfaceArea = other.bounds.size.x * other.bounds.size.z;
			float waterLevel = transform.position.y + GetComponent<Collider> ().bounds.extents.y;
		
			float optimalDepth = ((-other.attachedRigidbody.mass) / (waterDensity * bottomSurfaceArea) + waterLevel);

			other.transform.position = new Vector3(other.transform.position.x, Mathf.Lerp (other.transform.position.y, optimalDepth, shipWaviness), other.transform.position.z);

		}
	
	}

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

	// Update is called once per frame
	void Update () {

		//+(transform.position.z/10) is a placeholder to offset the wave timing until some procedural wave generator is created
		difference = (Mathf.Sin (Mathf.Repeat (Time.time + (transform.localPosition.z/waveSize), 2 * Mathf.PI ) * waveFrequency) * waveHeight - transform.position.y) * Vector3.up ;


		transform.Translate (difference);
	}
}
