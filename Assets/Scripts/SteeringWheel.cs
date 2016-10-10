using UnityEngine;
using System.Collections;

public class SteeringWheel : MonoBehaviour {

	private Renderer rend;
	private Color red =  new Color (1, 0, 0, 1);
	public Color boat_color;
	public bool seated = false;
	private Vector3 oldPosition;

	private Vector3 movement;
	private Vector3 forwardMovement;
	private Vector3 rotation;
	public float speed;
	public float rotationSpeed;
	private GameObject sail;
	private GameObject player;
	private bool playerInRange;
	private Rigidbody boat;

	void Start(){

		rend = GetComponent<Renderer> ();
		sail = GameObject.Find ("Mast");
		boat = GetComponentInParent<Rigidbody>();
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Player") {
			player = other.gameObject;
			playerInRange = true;
		}
	}

	void OnTriggerStay (Collider other) {
		if (other.tag == "Player")
		{
			rend.material.color = red;
		}
	}


	void Update()
	{
		if(playerInRange){
			if (Input.GetButtonDown("Submit")) {
				if (seated) {
					seated = false;
					player.transform.localPosition = oldPosition;
					player.transform.SetParent (null);

					player.GetComponent<CharacterController> ().enabled = true;

					return;
				} 
				else if (!seated) {
					seated = true;
					player.transform.SetParent (transform);
					oldPosition = new Vector3 (player.transform.localPosition.x, player.transform.localPosition.y, player.transform.localPosition.z);
					player.transform.localPosition = transform.localPosition - new Vector3 (0f, 0f, 2f);

					player.GetComponent<CharacterController> ().enabled = false;

					return;
				}
			}
		}

	}


	void FixedUpdate()
	{

		if (seated && sail.GetComponent<SailController>().sailUp) {

			forwardMovement = new Vector3 (0f, 0f, Input.GetAxis("Vertical"));
			rotation = new Vector3 (0f, Input.GetAxis ("Horizontal"), 0f);

			boat.AddRelativeForce (forwardMovement * speed);
			boat.AddRelativeTorque(rotation * rotationSpeed);

		}

	}

	void OnTriggerExit (Collider other){
		if (other.tag == "Player") 
		{
			rend.material.color = boat_color;
			player = null;
			playerInRange = false;
		}
	}

}

