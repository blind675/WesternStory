using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveScene : MonoBehaviour {

	public GameObject infoText;
	public StoryController storyController;

	private void OnTriggerEnter (Collider other)
	{
		if (WeaponsController.hasKnife || WeaponsController.hasGun || WeaponsController.hasDoubleGun) {
			if (StoryController.CanLeaveScene ()) {

				storyController.GoToNextScene ();


				InputManager.moveForward = false;
				InputManager.moveBackwards = false;
				InputManager.rotateCCW = false;
				InputManager.rotateCW = false;
				InputManager.fire = false;

			} else {
				infoText.SetActive (true);
				infoText.GetComponent<Text> ().text = "Can't leave yet. Talk to " + StoryController.GetTriggerNPCName ();
				infoText.GetComponent<Text> ().color = Color.red;
			}
		} else {

			infoText.SetActive (true);
			infoText.GetComponent<Text> ().text = "Don't leave Town without a weapon!";
			infoText.GetComponent<Text> ().color = Color.red;
		}

	}

	private void OnTriggerExit (Collider other)
	{

		infoText.SetActive (false);
	}
}
