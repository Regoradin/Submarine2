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

	//void OnTriggerStay(Collider other)
	//{

	//	//Z-ROTATION: Finds the top and bottom height of the ship cube in the middle of the wave after the rotation about the z axis has been applied

	//	float lowest_point_x = -1;
	//	float highest_point_x = -1;
	//	//finds the x value of the lowest and highest points on the floating box by finding the corner of the bounding box in world coords and then doing some trig. Extents are added or subtracted if angle is pos/negative
	//	if (other.transform.eulerAngles.z <= 180)
	//	{
	//		lowest_point_x = other.transform.position.x - other.bounds.extents.x + (Mathf.Cos((Mathf.PI / 2) - (other.transform.eulerAngles.z * Mathf.Deg2Rad)) * other.transform.localScale.y);
	//		highest_point_x = other.transform.position.x + other.bounds.extents.x - (Mathf.Cos((Mathf.PI / 2) - (other.transform.eulerAngles.z * Mathf.Deg2Rad)) * other.transform.localScale.y);
	//	}
	//	if (other.transform.eulerAngles.z > 180)
	//	{
	//		lowest_point_x = other.transform.position.x + other.bounds.extents.x + (Mathf.Cos((Mathf.PI / 2) - (other.transform.eulerAngles.z * Mathf.Deg2Rad)) * other.transform.localScale.y);
	//		highest_point_x = other.transform.position.x - other.bounds.extents.x - (Mathf.Cos((Mathf.PI / 2) - (other.transform.eulerAngles.z * Mathf.Deg2Rad)) * other.transform.localScale.y);
	//	}

	//	//does a similar calculation as the above to find the distance from the lowest and highest points on the box to the center of the water
	//	float distance_from_lowest_point_to_water_x = transform.position.x - lowest_point_x;
	//	float distance_from_highest_point_to_water_x = transform.position.x - highest_point_x;

	//	//calculates the height of other at the middle of the water. If the object does not actually exist at the center of the water, it will calculated as if it was extended along the x axis. 
	//	float bottom_height = other.transform.position.y - other.bounds.extents.y + (Mathf.Tan(Mathf.Deg2Rad * other.transform.eulerAngles.z) * distance_from_lowest_point_to_water_x);
	//	float top_height = other.transform.position.y + other.bounds.extents.y + (Mathf.Tan(Mathf.Deg2Rad * other.transform.eulerAngles.z) * distance_from_highest_point_to_water_x);
		
	//	//Ensures that the max height is not higher than the surface of the water itself.
	//	if (top_height > transform.position.y + transform.localScale.y/2)
	//	{
	//		top_height = transform.position.y + transform.localScale.y/2;
	//	}

	//	Debug.Log(other.name + " bottom_height first method " + bottom_height);

	//	//SECOND METHOD
	//	//totally unrotated
	//	bottom_height = other.transform.position.y - other.transform.localScale.y / 2;
	//	Debug.Log("bottom_height unrotated " + bottom_height);

	//	//Z-ROTATION:
	//	float x_difference = other.transform.position.x - transform.position.x;

	//	float y_difference = other.transform.position.y - bottom_height;

	//	float unrotated_z = Mathf.Atan(y_difference / x_difference);
	//	float rotated_z = unrotated_z + (other.transform.eulerAngles.z * Mathf.Deg2Rad);

	//	bottom_height = other.transform.position.y - (Mathf.Tan(rotated_z) * x_difference);

	//	Debug.Log("bottom_height pre-adjust " + bottom_height);
	//	//adjusts by the amount that this method is wrong. complicated geometry ensues.
	//	float adjustment = (Mathf.Sin(unrotated_z) * ((x_difference / Mathf.Cos(rotated_z)) - (x_difference / Mathf.Cos(unrotated_z))))/Mathf.Sin(Mathf.PI/2-other.transform.eulerAngles.z);
	//	bottom_height = bottom_height + adjustment;


	//	Debug.Log("adjustment " + adjustment);
	//	Debug.Log("bottom_height z rotated " + bottom_height);




	//	//X-ROTATION: Adjusts the above top and bottom height after a z rotation is applied.

	//	float z_difference = other.transform.position.z - transform.position.z;

	//	y_difference = other.transform.position.y - bottom_height;

	//	float unrotated_x = Mathf.Atan(y_difference / z_difference);
	//	float rotated_x = unrotated_x + (other.transform.eulerAngles.x * Mathf.Deg2Rad);

	//	bottom_height = other.transform.position.y - (Mathf.Tan(rotated_x) * z_difference);

	//	//Debug.Log("z_diff " + z_difference);
	//	//Debug.Log("y_diff " + y_difference);
	//	//Debug.Log("unrotated_x " + unrotated_x);
	//	//Debug.Log("rotated_x " + rotated_x);
	//	//Debug.Log("trigg stuff " + (Mathf.Tan(rotated_x) * z_difference));
	//	//Debug.Log("bottom_height x rotated " + bottom_height);


	//}

	//A newerer, even bettererer method of doing things, which will work even more goodly.
	//This new system doesn't go on the wave prefab, but is instead instantiated by the water script and then runs on a seperate prefab parented to the wave.
	void OnTriggerStay(Collider other)
	{

	}
}
