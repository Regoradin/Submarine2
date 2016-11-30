using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {

	// A new floating script that will be made to work when applied to waves, which should be make everything a lot more efficient, especially for smaller ships

	void OnTriggerStay(Collider other)
	{
		float difference_x = transform.position.x - other.transform.position.x;
		float difference_y = transform.position.y - other.transform.position.y;
		float difference_z = transform.position.z - other.transform.position.z;

		//does some math. document this later. this is mostly on the local grid if i did this right.
		Vector3 difference_on_local_grid_to_lowest_point_make_this_name_less_bad = new Vector3(0, 0, 0) - ((difference_x / Mathf.sin(other.transform.eulerAngles.y) / Mathf.sin(other.tranform.eulerAngles.z)), thing, difference_z / Mathf.sin(other.transform.eulerAngles.y) / Mathf.sin(other.tranform.eulerAngles.x)));

		//turn the above mess into world coords, apply that tranformation to the centerpoint, and then take the y value from that and badabing badaboom you get a thing
	}
}
