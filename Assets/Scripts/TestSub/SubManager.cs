using UnityEngine;
using System.Collections;

public class SubManager : MonoBehaviour {

	private Rigidbody rb;

	public Vector3 relative_center_of_mass;

	void Start () {

		rb = GetComponent<Rigidbody>();

		rb.centerOfMass = relative_center_of_mass;

	}

	void Update () {
	
	}
}
