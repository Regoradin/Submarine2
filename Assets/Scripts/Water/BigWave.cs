using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigWave : MonoBehaviour {

	public GameObject OceanManager;
	private OceanManager oceanManager;

	private float waveHeight;
	private float waveSize;
	private float waveSpeed;
	private float waveAngle;

	private Dictionary<string, List<float>> oldWaveProperties = new Dictionary<string, List<float>>();


	void Start(){

		OceanManager = GameObject.Find ("Ocean Manager");

		oceanManager = OceanManager.GetComponent<OceanManager>();

		GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Sin(transform.localEulerAngles.y * Mathf.Deg2Rad), 0f, Mathf.Cos(transform.localEulerAngles.y * Mathf.Deg2Rad)) * -waveSpeed;
	}

	public void SetBigWaveProperties(float height, float size, float speed)
	{
		waveHeight = height;
		waveSize = size;
		waveSpeed = speed;

		//Adjusts scale and position to make the bigWave only cover the half of the sin wave that is on the positive side, ie. only the bit that would make a bigger wave.
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, size * Mathf.PI);
		transform.Translate(new Vector3(0f, 0f, size * Mathf.PI / 2));
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Water"){

			List<float> waveProperties = new List<float> ();
			waveProperties.Add (other.GetComponent<Water> ().waveHeight);
			waveProperties.Add (other.GetComponent<Water> ().waveSize);
			waveProperties.Add (other.GetComponent<Water>().waveAngle * Mathf.Rad2Deg);
			waveProperties.Add (other.GetComponent<Water> ().waveSpeed);

			oldWaveProperties[other.ToString()] = waveProperties;

			oceanManager.SetWaveProperties(other.gameObject, waveHeight, waveSize, transform.eulerAngles.y, waveSpeed);
			

		}
	}

	void OnTriggerExit (Collider other){
		if (other.tag == "Water")
		{

			List<float> waveProperties = new List<float>();
			waveProperties = oldWaveProperties[other.ToString()];

			oceanManager.SetWaveProperties(other.gameObject, waveProperties[0], waveProperties[1], waveProperties[2], waveProperties[3]);

		}
	}

}
