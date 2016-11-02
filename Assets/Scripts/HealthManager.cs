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
			health = value;
			if(health > maxHealth)
			{
				health = maxHealth;
			}
			if (health < 0)
			{
				health = 0;
			}
		}
	}
	
	private Color defaultColor;

	private Renderer rend;
	
	void Start()
	{
		health = maxHealth;
		rend = GetComponent<Renderer>();
		defaultColor = rend.material.color;
	}

	void Update () {

		if(health <= maxHealth / 2)
		{
			rend.material.color = new Color(1, 0, 0, 1);
		}

		if(health == 0)
		{
			Destroy(gameObject);
		}

		else
		{
			rend.material.color = defaultColor;
		}

	}
}
