using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	public GameObject ammo;

	void Shoot(GameObject ammo) {
		Debug.Log("shooting!");
		//Makes the shot exist
		GameObject shot = (GameObject)Instantiate(ammo, transform.position, transform.localRotation);

		//Makes it go fast
		Rigidbody shot_rb = shot.GetComponent<Rigidbody>();
		shot_rb.AddRelativeForce(new Vector3(1000f, 0f));
	}

	void Start()
	{
		Shoot(ammo);
	}
}
