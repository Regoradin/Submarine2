using UnityEngine;
using System.Collections;

public class NewFloat : MonoBehaviour {

	// A new floating script that will be made to work when applied to waves, which should be make everything a lot more efficient, especially for smaller ships

	void OnTriggerStay(Collider other)
	{
		//other_angle_rad contains the floating objects orientation in radians so it plays nice with the trig stuff later on.
		Vector3 other_angle_rad = new vector3(
			Mathf.Deg2Rad * other.transform.eulerAngles.x,
			Mathf.Deg2Rad * other.transform.eulerAngles.y,
			Mathf.Deg2Rad * other.transform.eulerAngles.z);

		//this math is all a whole mess of triangles and trig that ends with local_difference, a vector3 that when subtracted from the floating object's position on the local grid,
		//will give the coords of the lowest point on the object in local space.
		float difference_x = transform.position.x - other.transform.position.x;
		float difference_y = transform.position.y - other.transform.position.y;
		float difference_z = transform.position.z - other.transform.position.z;

		Vector3 local_difference = new Vector3(difference_x / Mathf.Sin(other_angle_rad.y) / Mathf.Sin(other_angle_rad.z),
			other.transform.lossyScale.y/2, 
			difference_z / Mathf.Sin(other_angle_rad.y) / Mathf.Sin(other_angle_rad.x));

		Vector3 lowest_point = new Vector3(other.transform.localPosition.x - local_difference.x, other.transform.localPosition.y - local_difference.y, other.transform.localPosition.z - local_difference.z);

		Debug.Log(other.name + " " + other.transform.TransformPoint(lowest_point).y);
	}
}
