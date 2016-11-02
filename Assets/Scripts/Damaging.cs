using UnityEngine;
using System.Collections;

public class Damaging : MonoBehaviour {

	public int damage;

	private float minSpeed = 0f;

	void Start () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		Destroy(gameObject);
		if(collision.relativeVelocity.magnitude >= minSpeed)
		{
			collision.collider.gameObject.GetComponent<HealthManager>().Health -= damage;
			Debug.Log(collision.collider.gameObject.GetComponent<HealthManager>().Health);
		}

	}
}
