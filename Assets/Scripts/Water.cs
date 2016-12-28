using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	public float waveHeight;
	public float waveSize;
	public float waveSpeed;
	public float waveAngle;

	private GameObject player;
	private OceanManager oceanManager;

	public GameObject TrackerBox;
	private Tracker tracker;

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

			tracker.colliding = true;
			Debug.Log("colliding = true");
		}

	}

	void OnTriggerExit (Collider other){
		if (other.tag == "Floatable") {
			if (other.attachedRigidbody)
				other.attachedRigidbody.angularDrag = old_angular_drag;
				other.attachedRigidbody.drag = oldDrag;
		}

		tracker.colliding = false;
		Debug.Log("Colldiing = false");

	}

	void Start (){		
		player = GameObject.Find ("Player");
		oceanManager = GetComponentInParent<OceanManager> ();

		waveAngle = waveAngle * Mathf.Deg2Rad;

		transform.name = "Wave x: " + (transform.position.x/transform.localScale.x).ToString () +" z: " + (transform.position.z/transform.localScale.z).ToString();

		GameObject trackerBox = (GameObject)Instantiate(TrackerBox);
		trackerBox.transform.localScale = new Vector3(transform.localScale.x, .1f, transform.localScale.z);
		trackerBox.transform.parent = transform;
		tracker = GetComponentInChildren<Tracker>();
	}

	void Update () {
		
		if (Mathf.Abs (player.transform.position.x - transform.position.x) > (oceanManager.initialOceanX + 1) * transform.localScale.x  || Mathf.Abs (player.transform.position.z - transform.position.z) > (oceanManager.initialOceanZ + 1) * transform.localScale.z)
			Destroy (gameObject);

		Vector3 difference = ((Mathf.Sin((Time.time * waveSpeed + (Mathf.Cos(waveAngle) * transform.position.z + Mathf.Sin(waveAngle) * transform.position.x))/ waveSize) * waveHeight) - transform.position.y) * Vector3.up;
		
		transform.Translate (difference);
	}
}