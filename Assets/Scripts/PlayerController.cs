using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Transform playerTransform;
	public CharacterController characterController;

	// Player settings
	public float moveSpeed;

	// Update is called once per frame
	void Update ()
	{
		GetInput ();
	}

	void GetInput ()
	{
		if (InputManager.moveForward || Input.GetKey (KeyCode.W)) {
			characterController.Move (playerTransform.forward * moveSpeed * 0.1f);
		}

		if (InputManager.moveBackwards || Input.GetKey (KeyCode.S)) {
			characterController.Move (playerTransform.forward * moveSpeed * -0.1f);
		}

		if (InputManager.rotateCW || Input.GetKey (KeyCode.D)) {
			playerTransform.Rotate (playerTransform.up, 1);
		}

		if (InputManager.rotateCCW || Input.GetKey (KeyCode.A)) {
			playerTransform.Rotate (playerTransform.up, -1);
		}

		if (InputManager.fire) {
			//Debug.Log ("Move foreward");
		}

		characterController.SimpleMove (playerTransform.up * -1);
	}
}
