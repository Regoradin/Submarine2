using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BigWave : MonoBehaviour {

	public GameObject OceanManager;
	private OceanManager oceanManager;

	private Dictionary<string, List<float>> oldWaveProperties;


	void Start(){

		OceanManager = GameObject.Find ("Ocean Manager");

		oceanManager = OceanManager.GetComponent<OceanManager>();

		Dictionary<string, List<float>> oldWaveProperties = new Dictionary<string, List<float>> ();

	}

	void OnTriggerEnter (Collider other){
		if (other.tag == "Water"){

			List<float> waveProperties = new List<float> ();
			waveProperties.Add (other.GetComponent<Water> ().waveHeight);
			waveProperties.Add (other.GetComponent<Water> ().waveSize);
			waveProperties.Add (other.GetComponent<Water> ().waveAngle);
			waveProperties.Add (other.GetComponent<Water> ().waveSpeed);

			oldWaveProperties.Add (other.ToString, waveProperties);

			oceanManager.SetWaveProperties (other.gameObject, 100f, 20f, 0f, 1f);

		}
	}

	void OnTriggerExit (Collider other){

		List<float> waveProperties = new List<float> ();
		waveProperties = oldWaveProperties.TryGetValue(other.ToString, out);


		oceanManager.SetWaveProperties(other.gameObject, oldWaveProperties.

	}

}
