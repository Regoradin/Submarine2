﻿using UnityEngine;
using System.Collections;

public class SailController : MonoBehaviour {

	private Animator anim;
	private GameObject chair;
	public bool sailUp;

	void Start(){
	
		anim = GetComponentInChildren<Animator> ();

	}

	void OnTriggerStay( Collider other){
		if (other.tag == "Player") {

			if (Input.GetButtonDown ("Submit")) {
				anim.SetTrigger ("Sail Switch");

				}
			if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sail Up")){

				sailUp = true;

			}
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName ("Sail Up")) {

				sailUp = false;
			}
		}
	}

	public void SwitchSail()
	{
		anim.SetTrigger("Sail Switch");

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sail Up"))
		{

			sailUp = true;

		}
		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Sail Up"))
		{

			sailUp = false;
		}
	}
}
