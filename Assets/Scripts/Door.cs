using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	void OpenDoor ()
	{
		StartCoroutine (OpenDoorAnimation ());
	}

	IEnumerator OpenDoorAnimation ()
	{
		gameObject.GetComponent<Animator> ().enabled = true;
		yield return new WaitForSeconds (1);
		gameObject.GetComponent<Animator> ().enabled = false;
		yield return new WaitForSeconds (3);
		gameObject.GetComponent<Animator> ().enabled = true;
		yield return new WaitForSeconds (1);
		gameObject.GetComponent<Animator> ().enabled = false;
	}
}
