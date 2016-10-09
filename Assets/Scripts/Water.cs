using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	public float waveHeight;
	public float waveSize;
	public float waveSpeed;
	public float waveAngle;

	private GameObject player;
	private OceanManager oceanManager;

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

	void Start(){		
		player = GameObject.Find ("Player");
		oceanManager = GetComponentInParent<OceanManager> ();

		waveAngle = waveAngle * Mathf.Deg2Rad;

		transform.name = "Wave x: " + (transform.position.x/transform.localScale.x).ToString () +" z: " + (transform.position.z/transform.localScale.z).ToString();
	}

	void Update () {
		
		if (Mathf.Abs (player.transform.position.x - transform.position.x) > (oceanManager.initialOceanX + 1) * transform.localScale.x  || Mathf.Abs (player.transform.position.z - transform.position.z) > (oceanManager.initialOceanZ + 1) * transform.localScale.z)
			Destroy (gameObject);

		Vector3 difference = ((Mathf.Sin((Time.time * waveSpeed + (Mathf.Cos(waveAngle) * transform.position.z + Mathf.Sin(waveAngle) * transform.position.x))/ waveSize) * waveHeight) - transform.position.y) * Vector3.up;
		
		transform.Translate (difference);
	}
}