using UnityEngine;
using System.Collections;

public class BodyRotator : MonoBehaviour {

	public HeadRotator headRotator;

	void Start(){

		headRotator = GameObject.Find ("Head").GetComponent<HeadRotator> ();

	}

	void Update () {
	
		transform.Rotate (0.0f, Input.GetAxis("Mouse X") * headRotator.looksensitivity, 0.0f, Space.World);

	}
}
