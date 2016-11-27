using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {

	public int maxHealth;
	private int health;
	public int Health
	{
		get
		{
			return health;
		}
		set
		{
			if (!unbreakable) {
				health = value;
				if (health > maxHealth)
				{
					health = maxHealth;
				}
				if (health < 0)
				{
					health = 0;
				}
			}
		}
	}
	public bool unbreakable = false;
	public Color damaged = new Color(1, 0, 0);

	public int damage;
	private float minSpeed =0f;
	
	private Color defaultColor;

	private Renderer rend;
	
	void Start()
	{
		health = maxHealth;
		rend = GetComponent<Renderer>();
		defaultColor = rend.material.color;
	}

	void Update () {

		if (health == 0)
		{
			Destroy(gameObject);
		}

		else if (health <= maxHealth / 2)
		{
			rend.material.color = damaged;
		}

		else
		{
			rend.material.color = defaultColor;
		}

	}

	void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("Unchecked collision between" + gameObject.name + "and" + collision.contacts[0].otherCollider.gameObject.name);
		if (collision.gameObject.GetComponent<HealthManager>())
		{
			Debug.Log("Collision between" + gameObject.name + "and" + collision.gameObject.name);
			if (collision.relativeVelocity.magnitude >= minSpeed && collision.collider.gameObject.GetComponent<HealthManager>())
			{
				collision.gameObject.GetComponent<HealthManager>().Health -= damage;
				Debug.Log(collision.gameObject.name + " health: " + collision.gameObject.GetComponent<HealthManager>().Health);
			}
		}
	}
}
