using UnityEngine;
using System.Collections;

public class NewFloat : MonoBehaviour {

	public float water_density;
	// A new floating script that will be made to work when applied to waves, which should be make everything a lot more efficient, especially for smaller ships

	//void OnTriggerStay(Collider other)
	//{
	//	Collider other_collider = other.GetComponent<Collider>();

	//	float water_height = transform.position.y + transform.localScale.y/2;

	//	float lowest_height = other.transform.position.y - other_collider.bounds.extents.y;
	//	float highest_height = other.transform.position.y + other_collider.bounds.extents.y;

	//	float sunk_height = 0f;
	//	//the first if statement checks if the top of the object is below the water level and if so subtracts the height of the water above the object from the object, otherwise it's just the whole height.
	//	if (highest_height < water_height)
	//	{
	//		sunk_height = water_height - lowest_height - (water_height - highest_height);
	//	}
	//	else
	//	{
	//		sunk_height = water_height - lowest_height;
	//	}
	//	float sunk_volume = transform.localScale.x * transform.localScale.z * sunk_height;
		
	//	Vector3 bouyancy_force = sunk_volume* water_density * -Physics.gravity;

	//	//the bouyancy force is applied at the height of the center of bouyancy, but directly above the water block that is providing the bouyancy. any weirdness from this should be minimalized with larger ships.
	//	other.attachedRigidbody.AddForceAtPosition(bouyancy_force, new Vector3(transform.position.x, other_collider.bounds.center.y, transform.position.z));

	//}

	
	//An even newer floating script that will do work betterer.

	void OnTriggerStay(Collider other)
	{
		float lowest_point_x = -1;
		float highest_point_x = -1;
		//finds the x value of the lowest and highest points on the floating box by finding the corner of the bounding box in world coords and then doing some trig. Extents are added or subtracted if angle is pos/negative
		if (other.transform.eulerAngles.z <= 180)
		{
			lowest_point_x = other.transform.position.x - other.bounds.extents.x + (Mathf.Cos((Mathf.PI / 2) - (other.transform.eulerAngles.z * Mathf.Deg2Rad)) * other.transform.localScale.y);
			highest_point_x = other.transform.position.x + other.bounds.extents.x - (Mathf.Cos((Mathf.PI / 2) - (other.transform.eulerAngles.z * Mathf.Deg2Rad)) * other.transform.localScale.y);
		}
		if (other.transform.eulerAngles.z > 180)
		{
			lowest_point_x = other.transform.position.x + other.bounds.extents.x + (Mathf.Cos((Mathf.PI / 2) - (other.transform.eulerAngles.z * Mathf.Deg2Rad)) * other.transform.localScale.y);
			highest_point_x = other.transform.position.x - other.bounds.extents.x - (Mathf.Cos((Mathf.PI / 2) - (other.transform.eulerAngles.z * Mathf.Deg2Rad)) * other.transform.localScale.y);
		}

		//does a similar calculation as the above to find the distance from the lowest and highest points on the box to the center of the water
		float distance_from_lowest_point_to_water_x = transform.position.x - lowest_point_x;
		float distance_from_highest_point_to_water_x = transform.position.x - highest_point_x;

		//calculates the height of other at the middle of the water. If the object does not actually exist at the center of the water, it will calculated as if it was extended along the x axis. 
		float bottom_height = other.transform.position.y - other.bounds.extents.y + (Mathf.Tan(Mathf.Deg2Rad * other.transform.eulerAngles.z) * distance_from_lowest_point_to_water_x);
		float top_height = other.transform.position.y + other.bounds.extents.y + (Mathf.Tan(Mathf.Deg2Rad * other.transform.eulerAngles.z) * distance_from_highest_point_to_water_x);
		
		//Ensures that the max height is not higher than the surface of the water itself.
		if (top_height > transform.position.y + transform.localScale.y/2)
		{
			top_height = transform.position.y + transform.localScale.y/2;
		}

	}
}
