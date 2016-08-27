using UnityEngine;
using System.Collections;

public class OceanManager : MonoBehaviour {

	public GameObject wave;
	private GameObject player;

	public int initialOceanX;
	public int initialOceanZ;

	public int shiftX;
	public int shiftZ;

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

	}


	void Update(){

		float differenceX = player.transform.position.x - oldPosition.x;
		float differenceZ = player.transform.position.z - oldPosition.z;


		//Every time you go the length of one wave in a certain direction, it adds another line of waves to that side.
		if (differenceX > wave.transform.localScale.x) {
			shiftX += 1;
			for (int z = -initialOceanZ; z <= initialOceanZ; z++) {

				GameObject Wave = (GameObject)Instantiate (wave, new Vector3 ((shiftX + initialOceanX) * wave.transform.localScale.x, -wave.transform.localScale.y, (shiftZ + initialOceanZ) + z * wave.transform.localScale.z), Quaternion.identity);

			}
			oldPosition.x = player.transform.position.x;
		}
		if (-differenceX > wave.transform.localScale.x) {
			shiftX -= 1;
			for (int z = -initialOceanZ; z <= initialOceanZ; z++) {

				GameObject Wave = (GameObject)Instantiate (wave, new Vector3 ((shiftX - initialOceanX) * wave.transform.localScale.x, -wave.transform.localScale.y, (shiftZ + initialOceanZ) - z * wave.transform.localScale.z), Quaternion.identity);

			}
			oldPosition.x = player.transform.position.x;
		}

		if (differenceZ > wave.transform.localScale.z) {
			shiftZ += 1;
			for (int x = -initialOceanX; x <= initialOceanX; x++) {
				Debug.Log (x);

				GameObject Wave = (GameObject)Instantiate (wave, new Vector3 ((shiftX + initialOceanX) + x *  wave.transform.localScale.x, -wave.transform.localScale.y, (shiftZ + initialOceanZ) * wave.transform.localScale.z), Quaternion.identity);

			}
			oldPosition.z = player.transform.position.z;
		}
		if (-differenceZ > wave.transform.localScale.z) {
			shiftZ -= 1;
			for (int x = -initialOceanX; x <= initialOceanX; x++) {

				GameObject Wave = (GameObject)Instantiate (wave, new Vector3 ((shiftX - initialOceanX) - x  * wave.transform.localScale.x, -wave.transform.localScale.y, (shiftZ - initialOceanZ) * wave.transform.localScale.z), Quaternion.identity);

			}
			oldPosition.z = player.transform.position.z;
		}
			
	}
}
