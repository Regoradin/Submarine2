using UnityEngine;
using System.Collections;

public class NewFloat : MonoBehaviour {

	public float water_density;
	// A new floating script that will be made to work when applied to waves, which should be make everything a lot more efficient, especially for smaller ships

	void OnTriggerStay(Collider other)
	{
		Collider other_collider = other.GetComponent<Collider>();

		float water_height = transform.position.y + transform.localScale.y/2;

		float lowest_height = other.transform.position.y - other_collider.bounds.extents.y;
		float highest_height = other.transform.position.y + other_collider.bounds.extents.y;

		float sunk_height = 0f;
		//the first if statement checks if the top of the object is below the water level and if so subtracts the height of the water above the object from the object, otherwise it's just the whole height.
		if (highest_height < water_height)
		{
			sunk_height = water_height - lowest_height - (water_height - highest_height);
		}
		else
		{
			sunk_height = water_height - lowest_height;
		}
		float sunk_volume = transform.localScale.x * transform.localScale.z * sunk_height;
		
		Vector3 bouyancy_force = sunk_volume* water_density * -Physics.gravity;

		//the bouyancy force is applied at the height of the center of bouyancy, but directly above the water block that is providing the bouyancy. any weirdness from this should be minimalized with larger ships.
		other.attachedRigidbody.AddForceAtPosition(bouyancy_force, new Vector3(transform.position.x, other_collider.bounds.center.y, transform.position.z));

	}
}
