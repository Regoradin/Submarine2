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

		MakeBigWave(new Vector3 (4,0,2), 6, 20, 1f, 40f, 2f);

		player = GameObject.Find ("Player");
		oldPosition = player.transform.position;

	}

	//Changes settings on a wave, doesn't modify frequency because it is basically the same thing as size for now
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

	public void MakeBigWave (Vector3 center, int length, int width, float height, float angle, float speed){

		Quaternion angle_quat = (Quaternion) Quaternion.Euler (0f, angle, 0f);

		GameObject bigWave = (GameObject)Instantiate (BigWave, center, angle_quat);

		bigWave.transform.localScale = new Vector3 (width, 1f, length);


//		var random_wave = new GameObject ();
//		random_wave.transform.position = new Vector3(center.x, 0, center.y);
//		random_wave.transform.eulerAngles = new Vector3(0, angle, 0);
//
//		angle = angle * Mathf.Deg2Rad;
//
//		//creates integer side lengths to a hypotenuse of the desired wave angle
//		float yFactor = (1 / Mathf.Sin (angle));
//		float xFactor = (1 / Mathf.Cos (angle));
//
//
//		int xStep = Mathf.RoundToInt (Mathf.Cos (angle) * xFactor * yFactor);
//		int yStep = Mathf.RoundToInt (Mathf.Sin (angle) * yFactor * xFactor);
//
//		center = Vector2.Scale (center, new Vector2(wave.transform.localScale.x, wave.transform.localScale.z));



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
