using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {

	public float speed;
	public float looksensitivity;

	private GameObject chair;
	private CharacterController controller;
	private Rigidbody rb;

	void Start () {
		chair = GameObject.Find ("Steering Wheel");
		//controller = GetComponent<CharacterController> ();
		rb = GetComponent<Rigidbody>();
		
	}

	void Update()
	{
		//localVelocity reads the player input, and then worldVelocity transforms it to to direction of the body
		Vector3 localVelocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * speed;
		Vector3 worldVelocity = GameObject.Find("Body").transform.TransformVector(localVelocity);

		rb.velocity = worldVelocity;
	}
}
