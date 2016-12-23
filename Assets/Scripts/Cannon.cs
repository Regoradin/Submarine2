using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	public GameObject ammo;

	void Shoot(GameObject ammo) {
		Debug.Log("shooting!");
		GameObject shot = (GameObject)Instantiate(ammo, transform.position, Quaternion.identity);
		Rigidbody shot_rb = shot.GetComponent<Rigidbody>();
		shot_rb.AddForce(new Vector3(1000f, 0f));
	}

	void Start()
	{
		Shoot(ammo);
	}
}
