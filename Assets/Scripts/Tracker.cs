using UnityEngine;
using System.Collections;

public class Tracker : MonoBehaviour {

	private Collider parentCollider;
	private Water parentWater;

	public bool colliding = false;
	public float accuracy;

	public float top_height;
	public float bottom_height;
	
	void Start()
	{
		parentCollider = GetComponentInParent<Collider>();
		parentWater = parentCollider.GetComponentInParent<Water>();
	}

	void Update () {
		//if nothing is in the water or the tracker has left the water, set it back to the top.
			if (colliding == false || transform.localPosition.y < -.5)
			{
				transform.localPosition = new Vector3(0f, .5f, 0f);
			}
			else
			{
				//while the tracker is intersecting the boat currently floating boat move down
				while (parentWater.floating_collider.bounds.Intersects(GetComponent<Collider>().bounds) == false)
				{
					transform.Translate(Vector3.down * accuracy);
				}
				//once it does intersect however set the top height...
				top_height = transform.position.y;
				//and move down until it stops intersecting
				while (parentWater.floating_collider.bounds.Intersects(GetComponent<Collider>().bounds) == true)
				{
					transform.Translate(Vector3.down * accuracy);
				}
				//at which point set the bottom height and reset to the top
				bottom_height = transform.position.y;
				transform.localPosition = new Vector3(0f, .5f, 0f);
			}
	}

	//void OnTriggerEnter(Collider other)
	//{
	//	top_height = transform.position.y;
	//	//Debug.Log("top height " + top_height);
	//}

	//void OnTriggerExit(Collider other)
	//{
	//	bottom_height = transform.position.y;
	//	//Debug.Log("bottom height " + bottom_height);

	//	//This will make everything faster by resetting to the top after scanning the first objcet, but will eventually cause problems when multiple ships/bodies are passing through the same space, such as when subs go above or below each other or when things fall apart.
	//	transform.localPosition = new Vector3(0f, .5f, 0f);
	//}
}
