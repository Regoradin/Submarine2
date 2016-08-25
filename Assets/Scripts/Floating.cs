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


	void OnTriggerStay (Collider other){
		if (other.tag == "Floatable" && other.attachedRigidbody) {

			int sunkPoints = 0;
			int totalPoints = 0;
			float volume = GetComponent<Collider> ().bounds.size.x * GetComponent<Collider> ().bounds.size.y * GetComponent<Collider> ().bounds.size.z;

			//loops through 1000 test points in a 10x10x10 grid throughout the box and creates a Vector3 defining each point as a multiple of the full extents of the box.
			for (float x = -.5f; x <= .5f; x = x + .1f) {
				for (float y = -.5f; y <= .5f; y = y + .1f) {
					for (float z = -.5f; z <= .5f; z = z + .1f) {

						//testPoint may have to be defined by (other.bounds.size.x * x, other.bounds.size.y * y, other.bounds.size.z * z) for ships that are larger than 1 unscaled, although I may just use a scaled up cube collider to handle bouyancy
						Vector3 testPoint = new Vector3 (x, y, z);
						float waterLevel = transform.position.y + GetComponent<Collider> ().bounds.extents.y;

						if (other.transform.TransformPoint (testPoint).y < waterLevel)
							sunkPoints += 1;
						totalPoints += 1;
					}
				}
			}

			//Debug.Log ("Sunk: " + sunkPoints);
			//Debug.Log ("Total: " + totalPoints);

			other.attachedRigidbody.AddForceAtPosition (((sunkPoints / totalPoints) * volume * waterDensity * -Physics.gravity), other.transform.position);
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
