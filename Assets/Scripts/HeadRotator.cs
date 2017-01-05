using UnityEngine;
using System.Collections;

public class HeadRotator : MonoBehaviour {

	public float looksensitivity;
	private float verticalmouse;
	private Vector3 Rotation;
	private float yRotation;

	public float maxY;
	public float minY;


	void Update () {

		transform.Rotate (-Input.GetAxis("Mouse Y")* looksensitivity, Input.GetAxis("Mouse X") * looksensitivity, 0.0f, Space.Self);

		//a is the angle but fixed so it is -180 to 180 instead of 0 to 360
		float a = transform.eulerAngles.x;
		if (transform.eulerAngles.x > maxY + 10)
			a = transform.eulerAngles.x - 360;

		yRotation = Mathf.Clamp (a, minY, maxY);

		transform.eulerAngles = new Vector3 (yRotation, transform.eulerAngles.y, 0f);
	}
}
