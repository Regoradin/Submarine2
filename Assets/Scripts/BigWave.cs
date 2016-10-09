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

	}

	public void SetBigWaveProperties(float height, float size, float speed, float angle)
	{
		waveHeight = height;
		waveSize = size;
		waveSpeed = speed;
		waveAngle = angle;
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Water"){

			List<float> waveProperties = new List<float> ();
			waveProperties.Add (other.GetComponent<Water> ().waveHeight);
			waveProperties.Add (other.GetComponent<Water> ().waveSize);
			waveProperties.Add (other.GetComponent<Water> ().waveAngle);
			waveProperties.Add (other.GetComponent<Water> ().waveSpeed);

			oldWaveProperties[other.ToString()] = waveProperties;

			oceanManager.SetWaveProperties(other.gameObject, waveHeight, waveSize, waveAngle, waveSpeed);
			

		}
	}

	void OnTriggerExit (Collider other){
		if (other.tag == "Water")
		{

			List<float> waveProperties = new List<float>();
			waveProperties = oldWaveProperties[other.ToString()];

			Debug.Log(waveProperties[2]);
			oceanManager.SetWaveProperties(other.gameObject, waveProperties[0], waveProperties[1], waveProperties[2], waveProperties[3]);

		}
	}

}
