using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
	[SerializeField]
	GameObject goCreate;

	[SerializeField]
	float fTimeIntervals;

	[SerializeField]
	Vector3 v3SpawnPosJitter;

	[SerializeField]
	float fzMin = 7f;

	[SerializeField]
	float fzMax = 17f;

	float fTimer = 0;

	// Start is called before the first frame update
	void Start ()
	{
		fTimer = fTimeIntervals;
	}

	// Update is called once per frame
	void Update ()
	{
		fTimer -= Time.deltaTime;

		if (fTimer <= 0) {

			float xPos = v3SpawnPosJitter.x * (Random.value);

			while (fzMin < xPos && fzMax > xPos) xPos = v3SpawnPosJitter.x * (Random.value);

			fTimer = fTimeIntervals;
			Vector3 v3SpawnPos = transform.position;
			v3SpawnPos += Vector3.right * xPos * -1;
			v3SpawnPos += Vector3.forward * v3SpawnPosJitter.z * (Random.value - 0.5f);
			v3SpawnPos += Vector3.up * v3SpawnPosJitter.y * (Random.value);

			Instantiate (goCreate, v3SpawnPos, Quaternion.identity);
		}

	}
}
