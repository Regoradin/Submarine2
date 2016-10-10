using UnityEngine;
using System.Collections;

public class BoatManager : MonoBehaviour {

	private Rigidbody rigidBody;
	private SteeringWheel wheel;

	private float initialMass;

	public float ballastCapacity;
	public float ballastFillSpeed;

	void Start () {
		rigidBody = GetComponent<Rigidbody> ();

		rigidBody.centerOfMass = new Vector3 (0, -10, 0);

		wheel = GetComponentInChildren<SteeringWheel>();

		initialMass = rigidBody.mass;
	}

	void Update(){
		if (wheel.seated)
		{
			rigidBody.mass -= Input.GetAxis("Shift-Control") * ballastFillSpeed;

			if (rigidBody.mass < initialMass)
				rigidBody.mass = initialMass;
			if (rigidBody.mass > initialMass + ballastCapacity)
				rigidBody.mass = initialMass + ballastCapacity;
		}

	}
}
