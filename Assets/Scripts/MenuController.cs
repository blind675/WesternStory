using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public Button continueButton;
	public GameObject mainCanvas;
	public GameObject exitSceneCanvas;

	// Start is called before the first frame update
	void Start ()
	{
		continueButton.interactable = false;
	}

	public void NewGame ()
	{
		StoryController.storyStepIndex = 0;

		// change scene
		mainCanvas.SetActive (true);
		exitSceneCanvas.SetActive (true);

		StartCoroutine (LoadScene ());
	}

	public void Contiune ()
	{
		// change scene
		mainCanvas.SetActive (true);
		exitSceneCanvas.SetActive (true);

		StartCoroutine (LoadScene ());
	}

	IEnumerator LoadScene ()
	{
		yield return new WaitForSeconds (0.28f);

		SceneManager.LoadScene (StoryController.GetSceneName ());
	}
}
