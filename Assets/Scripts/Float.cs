using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

	private Rigidbody rigidBody;

	public float waterDensity;

	void Start(){

		rigidBody = GetComponentInParent<Rigidbody>();

		Debug.Log (rigidBody.name);

	}

	void OnTriggerStay (Collider other){
		if (other.tag == "Water") {


			int sunkPoints = 0;
			int totalPoints = 0;
			float volume = transform.lossyScale.x * transform.lossyScale.y * transform.lossyScale.z;

			//loops through 1331 test points in a 11x11x11 grid throughout the box and creates a Vector3 defining each point as a multiple of the full extents of the box.
			for (float x = -.5f; x <= .5f; x = x + .1f) {
				for (float y = -.5f; y <= .5f; y = y + .1f) {
					for (float z = -.5f; z <= .5f; z = z + .1f) {

						//testPoint may have to be defined by (other.bounds.size.x * x, other.bounds.size.y * y, other.bounds.size.z * z) for ships that are larger than 1 unscaled, although I may just use a scaled up cube collider to handle bouyancy
						Vector3 testPoint = new Vector3 (x, y, z);
						float waterLevel = other.transform.position.y + other.bounds.extents.y;

						if (transform.TransformPoint (testPoint).y < waterLevel)
							sunkPoints += 1;
						totalPoints += 1;
					}
				}
			}

			rigidBody.AddForceAtPosition (((sunkPoints / totalPoints) * volume * waterDensity * -Physics.gravity), transform.position);
		}
	}
}
