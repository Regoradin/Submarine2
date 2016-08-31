using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	public float waveFrequency;
	public float waveHeight;
	public float waveSize;
	private Vector3 difference;

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

		//+(transform.position.z/waveSize) is a placeholder to offset the wave timing until some procedural wave generator is created
		difference = (Mathf.Sin (Mathf.Repeat (Time.time + ((Mathf.Cos(waveAngle) * transform.position.z + Mathf.Sin(waveAngle) * transform.position.x)/waveSize), 2 * Mathf.PI ) * waveFrequency) * waveHeight - transform.position.y) * Vector3.up ;

		transform.Translate (difference);
	}
}