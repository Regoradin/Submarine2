﻿using UnityEngine;
using System.Collections;

public class BottomWater : MonoBehaviour
{
	public float accuracy;

	private Water water;

	void Start()
	{
		water = GetComponentInParent<Water>();
		//ensures accuracy isn't 0 as that causes crashes on the while loop
		if (accuracy <= 0)
		{
			accuracy = .1f;
		}
	}

	void Update()
	{
		if (water.colliding == false)
		{
			transform.localPosition = new Vector3(0, -.25f, 0);
			transform.localScale = new Vector3(1, .5f, 1);
		}
		else
		{
			//set the scale to very small for precise iteration
			transform.localScale = new Vector3(1, accuracy, 1);
			//sets the position to be at the bottom of the bounding box of the floating object, at the x and z of the wave

			//sets the position to be at the bottom of the collider minus a little buffer based off the accuracy because it was acting weird without it.
			transform.position = new Vector3(transform.position.x, (water.floating_collider.bounds.min.y - (accuracy * 50)), transform.position.z);

			//while it doesn't intersect the floating collider, move up until it does intersect the floating collider,
			//also ensures with every loop that the floating_collider is still actually within the parent collider to prevent infinite loops
			while (!GetComponent<Collider>().bounds.Intersects(water.floating_collider.bounds) && transform.parent.GetComponent<Collider>().bounds.Intersects(water.floating_collider.bounds))
			{
				transform.position += Vector3.up * accuracy;
			}

			//report the height world coords fo the wave of the bottom of the floating object/top of the bottomwater to the parent water
			float top_height = transform.position.y + transform.lossyScale.y / 2;
			water.bottom_height = top_height;

			//adjust position and scale to fit
			float newY = (-transform.parent.position.y - transform.parent.localScale.y / 2 - top_height) / 2 + top_height;
			float newScale = (-transform.parent.position.y - transform.parent.localScale.y / 2 - top_height) / 2;
			//converts newScale from global scale to local scale
			newScale = newScale / transform.parent.localScale.y * 2;

			transform.position = new Vector3(transform.position.x, newY, transform.position.z);
			transform.localScale = new Vector3(1, newScale, 1);

		}
	}

}
