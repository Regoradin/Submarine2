using UnityEngine;
using System.Collections;

public class Tracker : MonoBehaviour {

	public bool colliding = false;
	public float accuracy;

	public float top_height;
	public float bottom_height;
	
	void Update () {
		if (colliding == false || transform.localPosition.y < -.5)
		{
			transform.localPosition = new Vector3(0f, .5f, 0f);
		}
		else
		{
			transform.Translate(Vector3.down * accuracy);
			Debug.Log("HEIGHT DIFFERENCE " + (top_height - bottom_height));
		}
	}

	void OnTriggerEnter(Collider other)
	{
		top_height = transform.position.y;
		Debug.Log("top height " + top_height);
	}

	void OnTriggerExit(Collider other)
	{
		bottom_height = transform.position.y;
		Debug.Log("bottom height " + bottom_height);

		//This will make everything faster by resetting to the top after scanning the first objcet, but will eventually cause problems when multiple ships/bodies are passing through the same space, such as when subs go above or below each other or when things fall apart.
		transform.localPosition = new Vector3(0f, .5f, 0f);
	}
}
