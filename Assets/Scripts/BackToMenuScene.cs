using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuScene : MonoBehaviour {

	public float sceneDelay = 10f;

	// Start is called before the first frame update
	void Start ()
	{
		StartCoroutine (LoadMenuScene ());
	}

	IEnumerator LoadMenuScene ()
	{
		yield return new WaitForSeconds (sceneDelay);

		// TODO: chenge to menu scene
		StoryController.storyStepIndex = 0;

		SceneManager.LoadScene ("TownScene");

	}
}
