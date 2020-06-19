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
		// if can change scene do it
		// if storry 
	}

	private void OnTriggerExit (Collider other)
	{
		infoText.SetActive (false);
	}
}
