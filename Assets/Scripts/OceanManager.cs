using UnityEngine;
using System.Collections;

public class OceanManager : MonoBehaviour {

	public GameObject wave;
	private GameObject player;

	public int initialOceanX;
	public int initialOceanZ;

	public int shiftX;
	public int shiftZ;

	public GameObject BigWave;

	private Vector3 oldPosition;
	//private float differenceX;
	//private float differenceZ;


	void Start(){

		for (int x = -initialOceanX; x <= initialOceanX; x++) {
			for (int z = -initialOceanZ; z <= initialOceanZ; z++) {

				GameObject Wave = (GameObject) Instantiate (wave, new Vector3 (x * wave.transform.localScale.x, -wave.transform.localScale.y, z * wave.transform.localScale.z), Quaternion.identity);
				Wave.transform.parent = transform;
			}
		}

		shiftX = 0;
		shiftZ = 0;

		player = GameObject.Find ("Player");
		oldPosition = player.transform.position;

		MakeBigWave(new Vector3(0f, 0f, 0f), 20f, 40f, 20f, 4f, 10f, 10f);

	}

	//Changes settings on a wave, doesn't modify frequency because it is basically the same thing as size for now
	/// <summary>
	/// Changes the settings on an individual wave.
	/// </summary>
	/// <param name="Wave">The wave gameObject getting altered</param>
	/// <param name="height">The hight of the sin wave</param>
	/// <param name="size">The wavelength of the sin wave</param>
	/// <param name="angle">The angle across the x-z plane that the sin wave is going in degrees, 0 degrees is z+</param>
	/// <param name="speed">How fast the wave goes</param>
	public void SetWaveProperties (GameObject Wave, float height, float size, float angle, float speed){

		Water water = Wave.GetComponent<Water>();

		water.waveHeight = height;
		water.waveSize = size;
		water.waveSpeed = speed;

		angle = angle * Mathf.Deg2Rad;
		water.waveAngle = angle;

	}

	GameObject FindWave(int x, int z){

		GameObject foundWave = GameObject.Find("Wave x: " + (wave.transform.position.x/wave.transform.localScale.x).ToString () +" z: " + (wave.transform.position.z/wave.transform.localScale.z).ToString());
		if (foundWave == null)
			Debug.Log ("Can't find Wave x: " + (wave.transform.position.x / wave.transform.localScale.x).ToString () + " z: " + (wave.transform.position.z / wave.transform.localScale.z).ToString ()); 
		return foundWave;
	}

	public void MakeBigWave (Vector3 center, float angle, float length, float width, float height, float size, float speed)
	{
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = new Vector3(0f, angle, 0f);

		GameObject bigWave = (GameObject) Instantiate(BigWave, center, rotation);

		bigWave.transform.localScale = new Vector3(width, wave.transform.localScale.y * height, length);

		BigWave bigwave = bigWave.GetComponent<BigWave>();
		bigwave.SetBigWaveProperties(height, size, speed, angle);

	}

	void Update(){

		float differenceX = player.transform.position.x - oldPosition.x;
		float differenceZ = player.transform.position.z - oldPosition.z;


		//Every time you go the length of one wave in a certain direction, it adds another line of waves to that side.
		if (differenceX > wave.transform.localScale.x) {
			shiftX += 1;
			for (int z = -initialOceanZ; z <= initialOceanZ; z++) {

				GameObject Wave = (GameObject)Instantiate (wave, new Vector3 ((shiftX + initialOceanX) * wave.transform.localScale.x, -wave.transform.localScale.y, ((shiftZ) * wave.transform.localScale.z) + (z * wave.transform.localScale.z)), Quaternion.identity);
				Wave.transform.parent = transform;

			}
			oldPosition.x = player.transform.position.x;
		}

		if (-differenceX > wave.transform.localScale.x) {
			shiftX -= 1;
			for (int z = -initialOceanZ; z <= initialOceanZ; z++) {

				GameObject Wave = (GameObject)Instantiate (wave, new Vector3 ((-initialOceanX + shiftX) * wave.transform.localScale.x, -wave.transform.localScale.y, ((shiftZ) * wave.transform.localScale.z) + (z * wave.transform.localScale.z)), Quaternion.identity);
				Wave.transform.parent = transform;

			}
			oldPosition.x = player.transform.position.x;
		}


		if (differenceZ > wave.transform.localScale.z) {
			shiftZ += 1;
			for (int x = -initialOceanX; x <= initialOceanX; x++) {

				GameObject Wave = (GameObject)Instantiate (wave, new Vector3 (((shiftX) * wave.transform.localScale.x) + (x * wave.transform.localScale.x), -wave.transform.localScale.y, (shiftZ + initialOceanZ) * wave.transform.localScale.z), Quaternion.identity);
				Wave.transform.parent = transform;

			}
			oldPosition.z = player.transform.position.z;
		}

		if (-differenceZ > wave.transform.localScale.z) {
			shiftZ -= 1;
			for (int x = -initialOceanX; x <= initialOceanX; x++) {

				GameObject Wave = (GameObject)Instantiate (wave, new Vector3 (((shiftX) * wave.transform.localScale.x) + (x * wave.transform.localScale.x), -wave.transform.localScale.y, (-initialOceanZ + shiftZ) * wave.transform.localScale.z), Quaternion.identity);
				Wave.transform.parent = transform;
				
			}
			oldPosition.z = player.transform.position.z;
		}


	}
}
