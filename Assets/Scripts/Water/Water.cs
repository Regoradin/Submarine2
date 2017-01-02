using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	public float waveHeight;
	public float waveSize;
	public float waveSpeed;
	public float waveAngle;

	private GameObject player;
	private OceanManager oceanManager;

	public GameObject topWater;
	public GameObject bottomWater;
	[HideInInspector]
	public Collider floating_collider;

	[HideInInspector]
	public float top_height;
	[HideInInspector]
	public float bottom_height;
	[HideInInspector]
	public bool colliding = false;

	public float waterDensity;

	public float waterDrag;
	public float water_angular_drag;
	private float oldDrag;
	private float old_angular_drag;


	void OnTriggerEnter (Collider other){
		if (other.tag == "Floatable") {
			if (other.attachedRigidbody) {
				oldDrag = other.attachedRigidbody.drag;
				old_angular_drag = other.attachedRigidbody.angularDrag;

				other.attachedRigidbody.drag = waterDrag;
				other.attachedRigidbody.angularDrag = water_angular_drag;
			}

			colliding = true;
			floating_collider = other;
		}

	}

	void OnTriggerExit (Collider other){
		if (other.tag == "Floatable")
		{
			if (other.attachedRigidbody)
				other.attachedRigidbody.angularDrag = old_angular_drag;
			other.attachedRigidbody.drag = oldDrag;

			colliding = false;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Floatable")
		{
			float width = transform.localScale.x;
			float length = transform.localScale.z;
			float height = top_height - bottom_height;

			float volume = length * width * height;

			Vector3 bouyancy = volume * -Physics.gravity * waterDensity;
			other.GetComponentInParent<Rigidbody>().AddForceAtPosition(bouyancy, other.bounds.center);
		}

	}

	void Start (){		

		player = GameObject.Find ("Player");
		oceanManager = GetComponentInParent<OceanManager> ();

		waveAngle = waveAngle * Mathf.Deg2Rad;

		transform.name = "Wave x: " + (transform.position.x/transform.localScale.x).ToString () +" z: " + (transform.position.z/transform.localScale.z).ToString();

		//sets up and parents top and bottom waters
		GameObject top_water = Instantiate(topWater);
		GameObject bottom_water = Instantiate(bottomWater);
		top_water.transform.parent = transform;
		bottom_water.transform.parent = transform;
	}

	void Update () {
		//checks distance to player and maintains dimensions set out in oceanManager
		if (Mathf.Abs (player.transform.position.x - transform.position.x) > (oceanManager.initialOceanX + 1) * transform.localScale.x  || Mathf.Abs (player.transform.position.z - transform.position.z) > (oceanManager.initialOceanZ + 1) * transform.localScale.z)
			Destroy (gameObject);

		//moves wavelike
		Vector3 difference = ((Mathf.Sin((Time.time * waveSpeed + (Mathf.Cos(waveAngle) * transform.position.z + Mathf.Sin(waveAngle) * transform.position.x))/ waveSize) * waveHeight) - transform.position.y) * Vector3.up;
		transform.Translate (difference);
	}
}