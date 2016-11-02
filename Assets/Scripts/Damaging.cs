using UnityEngine;
using System.Collections;

public class Damaging : MonoBehaviour {

	public int damage;
	public bool destroyOnHit = true;

	private float minSpeed = 0f;

	void Start () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if (destroyOnHit) { Destroy(gameObject); }
		//Checks to see if the collision is fast enough and if the thing being collided has health.
		if(collision.relativeVelocity.magnitude >= minSpeed && collision.collider.gameObject.GetComponent<HealthManager>())
		{
			collision.collider.gameObject.GetComponent<HealthManager>().Health -= damage;
		}

	}
}
