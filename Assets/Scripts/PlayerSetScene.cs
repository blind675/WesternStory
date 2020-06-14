using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetScene : MonoBehaviour {

	private void Awake ()
	{
		if (StoryController.storyStepIndex == 0) {
			gameObject.transform.position = new Vector3 (19.57f, 0.592f, 8.35f);
			gameObject.transform.rotation = Quaternion.Euler (0, -140.372f, 0);
		}
	}



}
