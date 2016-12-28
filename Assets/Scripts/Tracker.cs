using UnityEngine;
using System.Collections;

public class Tracker : MonoBehaviour {

	public bool colliding = false;
	public float accuracy;

	public float top_height;
	public float bottom_height;
	
	void Update () {
		if (colliding == false)
		{
			transform.localPosition = new Vector3(0f, .5f, 0f);
		}
		else
		{
			//while (transform.localPosition.y >= -.5)
			//{
				Debug.Log("Things are happening!");
				transform.Translate(Vector3.down * accuracy);
			//}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		top_height = transform.position.y;
		Debug.Log("top_height = " + top_height);
	}

	void OnTriggerExit(Collider other)
	{
		bottom_height = transform.position.y;
		Debug.Log("bottom_height = " + bottom_height);

		//This will make everything faster by resetting to the top after scanning the first objcet, but will eventually cause problems when multiple ships/bodies are passing through the same space, such as when subs go above or below each other or when things fall apart.
		transform.localPosition = new Vector3(0f, .5f, 0f);
	}
}
