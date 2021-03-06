﻿using UnityEngine;
using System.Collections;

public class TopWater : MonoBehaviour {

	public float accuracy;

	private Water water;
	private Renderer rend;

	void Start()
	{
		water = GetComponentInParent<Water>();
		rend = GetComponent<Renderer>();
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
			transform.localPosition = new Vector3(0, .25f, 0);
			transform.localScale = new Vector3(1, .5f, 1);
		}
		else
		{
			//moves this bit so the bottom of the topWater is at the top of the wave
			transform.localScale = new Vector3(1, accuracy, 1);

			//sets the position to be at the top of the collider plus a little buffer based off the accuracy because it was acting weird without it.
			transform.position = new Vector3(transform.position.x, (water.floating_collider.bounds.max.y + (accuracy * 50)), transform.position.z);



			//while it doesn't intersect the floating collider, move down until it does intersect the floating collider,
			//also ensures with every loop that the floating_collider is still actually within the parent collider to prevent infinite loops
			while (!GetComponent<Collider>().bounds.Intersects(water.floating_collider.bounds) && transform.parent.GetComponent<Collider>().bounds.Intersects(water.floating_collider.bounds))
			{
				transform.position += Vector3.down * accuracy;
			}

			//report the height world coords fo the wave of the top of the floating object/bottom of the topwater to the parent water
			float bottom_height = transform.position.y - transform.lossyScale.y/2;
			water.top_height = bottom_height;

			//adjust position and scale to fit
			float newY = (transform.parent.position.y + transform.parent.localScale.y/2 - bottom_height) / 2 + bottom_height;
			float newScale = (transform.parent.position.y + transform.parent.localScale.y/2 - bottom_height)/ 2;
			//converts newScale from global scale to local scale
			newScale = newScale / transform.parent.localScale.y * 2;

			transform.position = new Vector3(transform.position.x, newY, transform.position.z);
			transform.localScale = new Vector3(1, newScale, 1);
			
		}

		rend.enabled = true;
		if(transform.localScale.y < .001f)
		{
			rend.enabled = false;
		}
		
	}

}
