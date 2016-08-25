using UnityEngine;
using System.Collections;

public class BodyRotator : MonoBehaviour {

	public HeadRotator headRotator;

	void Start(){

		headRotator = GameObject.Find ("Head").GetComponent<HeadRotator> ();

	}

	void Update () {
	
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, headRotator.transform.eulerAngles.y, transform.eulerAngles.z);

	}
}
