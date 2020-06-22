using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour {

	public GameObject storyCanvas;
	public GameObject mainCanvas;
	public GameObject exitSceneCanvas;

	public Image DialogImage;
	public Text DialogLine;

	public Sprite PlayerImage;

	public GameObject Joe;

	public static int storyStepIndex = 0;

	private int conversationLineIndex = 0;
	private NPC NPC;

	private static string storyJSONString = "{\"stroySteps\":[{\"SceneLock\":true,\"CurrentScene\":\"TownScene\",\"NextStoryTrigger\":\"Letter\",\"Letter\":[\"PLAYER- My mother went away but she left a letter, I should read it.\",\"Dear Bob, I`ve gone to your grandma for two months. I'll be back in time for your wedding. Please visit Mary, your fiance, she has something to ask of you.\"]},{\"SceneLock\":true,\"CurrentScene\":\"TownScene\",\"NextStoryTrigger\":\"Mary\",\"Mary\":[\"My lovely husband to be, I`m so glad you came. I lost Daisy, my cow ever since I was little! We cannot get married without her at the ceremony.\",\"Can you find her for me? She usually hangs around the bear cave outside town. I really hope she`s ok.\",\"PLAYER- Yes mam, I`ll gladly bring Betsy back.\"],\"Sheriff\":[\"Hi Bob. Nice day ain`t it? I heard Mary is looking for ya. You should see what that`s about.\"],\"GunSmith\":[\"Howdy. Want a new gun. We have all you need for the right price. The Ammo is free.\"]},{\"SceneLock\":false,\"CurrentScene\":\"TownScene\",\"NextScene\":\"CaveScene\",\"NextStoryTrigger\":\"Scene\",\"Mary\":[\"Please find my cow Daisy, we cannot get married without her. Search outside town.\"],\"Sheriff\":[\"You should find that damn cow if you ever want to get married. Maybe, also, get a weapon.\"],\"GunSmith\":[\"Howdy. Want a new gun. We have all you need for the right price. The Ammo is free.\"]},{\"SceneLock\":false,\"CurrentScene\":\"CaveScene\",\"NextScene\":\"TownScene\",\"NextStoryTrigger\":\"Daisy\",\"PrevStoryTrigger\":\"Scene\",\"Daisy\":[\"PLAYER- Daisy, here you are! Let's get home so I can finally marry Mary.\",\"Moooo!\"]},{\"SceneLock\":false,\"CurrentScene\":\"CaveScene\",\"NextScene\":\"TownScene\",\"NextStoryTrigger\":\"Scene\"},{\"SceneLock\":true,\"CurrentScene\":\"TownScene\",\"NextScene\":\"TownScene\",\"NextStoryTrigger\":\"Sheriff\",\"Sheriff\":[\"Holy Smoke, where have you been?! Out finding kettle? While you were away the bandit named Joe came to town. He killed people and kidnaped your fiance, Mary.\",\"No one knows where he`s from. It`s like he came from nowhere. We just know he`s called Joe.\",\"There are some rumors that he and his posse have a camp outside the town. You should go there if you want to get your fiance back. And buy a new gun.\"],\"GunSmith\":[\"Howdy. Want a new gun. We have all you need for the right price. The Ammo is free.\"]},{\"SceneLock\":false,\"CurrentScene\":\"TownScene\",\"NextScene\":\"BanditScene\",\"NextStoryTrigger\":\"Scene\",\"Sheriff\":[\"Find the bandit Joe and get your fiance back. He`s outside town.\"],\"GunSmith\":[\"Howdy. Want a new gun. We have all you need for the right price. The Ammo is free.\"]},{\"SceneLock\":false,\"CurrentScene\":\"BanditScene\",\"NextScene\":\"TownScene\",\"NextStoryTrigger\":\"Joe\",\"PrevStoryTrigger\":\"Scene\",\"Joe\":[\"Ouch, that hurt! You hurt my eye!\",\"PLAYER- Relax, it`s just a scratch.\",\"Just a scratch?! My eye looks like a cotton flower now! You will pay for this. I will take your lovely fiance somewhere you`ll never find her!\",\"PLAYER- JOE! Wow, where did he go?! I should go back home and ask around!\"]},{\"SceneLock\":false,\"CurrentScene\":\"BanditScene\",\"NextScene\":\"TownScene\",\"NextStoryTrigger\":\"Scene\"},{\"SceneLock\":true,\"CurrentScene\":\"TownScene\",\"NextScene\":\"TownScene\",\"NextStoryTrigger\":\"Letter\",\"Letter\":[\"PLAYER- Dear Mother. A lot has happened in the last few days. A bandit named Joe came out of nowhere and kidnaped Mary.\",\"PLAYER- I fought with him but he managed to disappear, taking my fiance with him.\",\"PLAYER- I guess I could say: `If it hadn't been for Cotton-Eye Joe / I`d been married a long time ago`\"],\"Sheriff\":[\"You defeated Joe! Congratulations!\",\"Unfortunately he disappeared and took Mary with him. I`m sorry but it seems you still won't get married after all.\",\"Perhaps you should write a letter to your mother about this.\"],\"GunSmith\":[\"Howdy. Want a new gun. We have all you need for the right price. The Ammo is free.\"]}]}";

	private static StorySteps stroy;

	private void Start ()
	{
		stroy = JsonUtility.FromJson<StorySteps> (storyJSONString);
	}

	public bool CanTalkToNPC (string npcName)
	{
		string nextLine = GetLineOfConversation (npcName);
		//Debug.Log ("- next dialog Line : " + nextLine);

		if (nextLine == null) {
			return false;
		} else {
			return true;
		}

	}

	public bool CanOpenDoor ()
	{
		if (storyStepIndex == 0) {
			return false;
		} else {
			return true;
		}
	}

	public static bool CanLeaveScene ()
	{
		return !stroy.stroySteps [storyStepIndex].SceneLock;
	}

	public void GoToNextScene ()
	{
		Debug.Log (" - STORY - storyStepIndex: " + storyStepIndex);
		//Debug.Log (" - STORY - objectt: " + JsonUtility.ToJson (stroy.stroySteps [storyStepIndex], true));

		// Might be redundant check
		if (stroy.stroySteps [storyStepIndex].NextScene != SceneManager.GetActiveScene ().name) {

			// change scene
			mainCanvas.SetActive (false);
			storyCanvas.SetActive (false);
			exitSceneCanvas.SetActive (true);

			StartCoroutine (LoadNextScene ());
		}
	}

	IEnumerator LoadNextScene ()
	{
		yield return new WaitForSeconds (0.25f);

		SceneManager.LoadScene (stroy.stroySteps [storyStepIndex].NextScene);

		if (stroy.stroySteps [storyStepIndex].NextStoryTrigger == "Scene") {
			storyStepIndex++;
		} else if (stroy.stroySteps [storyStepIndex].PrevStoryTrigger == "Scene") {
			storyStepIndex--;
		}
	}

	public static string GetSceneName ()
	{
		return stroy.stroySteps [storyStepIndex].CurrentScene;
	}

	public static string GetTriggerNPCName ()
	{
		return stroy.stroySteps [storyStepIndex].NextStoryTrigger;
	}

	public static bool IsNPCInScene (NPC npc)
	{
		if (storyStepIndex == 0) return true;

		StoryStep currentStoryStep = stroy.stroySteps [storyStepIndex];

		switch (npc.NPCName) {

		case "Letter":
			if (currentStoryStep.Letter == null) return false;
			break;
		case "Mary":
			if (currentStoryStep.Mary == null) return false;
			break;
		case "Sheriff":
			if (currentStoryStep.Sheriff == null) return false;
			break;
		case "GunSmith":
			if (currentStoryStep.GunSmith == null) return false;
			break;
		case "Daisy":
			if (currentStoryStep.Daisy == null) return false;
			break;
		case "Joe":
			if (currentStoryStep.Joe == null) return false;
			break;
		}

		return true;

	}

	public void Click ()
	{
		SetNextDialogLine ();
	}

	public void TalkToNPC (NPC npc)
	{
		NPC = npc;
		conversationLineIndex = 0;

		mainCanvas.SetActive (false);
		storyCanvas.SetActive (true);

		//Debug.Log ("- talk to " + NPC.NPCName);

		SetNextDialogLine ();
	}

	IEnumerator LoadCreditsScene ()
	{
		yield return new WaitForSeconds (0.25f);

		SceneManager.LoadScene ("Credits");

		storyStepIndex = 0;
	}

	private void SetNextDialogLine ()
	{
		// close Joe extra dialog
		if (NPC == null) {
			// close dialog canvas
			mainCanvas.SetActive (true);
			storyCanvas.SetActive (false);

			return;
		}

		// get next line
		string nextDialogLine = GetLineOfConversation (NPC.NPCName);

		//Debug.Log ("- next dialog Line : " + nextDialogLine);

		if (nextDialogLine == null) {
			// if no next line
			//Debug.Log ("- CLOSE DIalOG");

			// close dialog canvas
			mainCanvas.SetActive (true);
			storyCanvas.SetActive (false);
			conversationLineIndex = 0;

			// evolve story if possible
			if (NPC.NPCName == stroy.stroySteps [storyStepIndex].NextStoryTrigger) {
				//Debug.Log ("- next story step");
				storyStepIndex++;
				if (storyStepIndex == 10) {
					//go to credits scene

					mainCanvas.SetActive (false);
					storyCanvas.SetActive (false);
					exitSceneCanvas.SetActive (true);

					StartCoroutine (LoadCreditsScene ());
				}
			}

		} else {
			// else

			// set the correct the correct dialog image
			// set the correct dialog line

			if (nextDialogLine.Contains ("PLAYER-")) {
				string correctString = nextDialogLine.Substring (8);

				if (nextDialogLine.Contains ("JOE")) {
					// hide Joe sprite;

					if (Joe != null) {
						SpriteRenderer spriteRenderer = Joe.transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ();
						spriteRenderer.enabled = false;
					}
				}

				DialogImage.sprite = PlayerImage;
				DialogLine.text = correctString;

			} else {
				DialogLine.text = nextDialogLine;
				DialogImage.sprite = NPC.NPCPicture;
			}

			// increment the dialog index
			conversationLineIndex++;
		}

	}

	private string GetLineOfConversation (string npcName)
	{
		StoryStep currentStoryStep = stroy.stroySteps [storyStepIndex];

		string returnString = null;

		switch (npcName) {

		case "Letter":
			if (currentStoryStep.Letter == null || currentStoryStep.Letter.Length - 1 < conversationLineIndex) return null;
			returnString = currentStoryStep.Letter [conversationLineIndex];
			break;
		case "Mary":
			if (currentStoryStep.Mary == null || currentStoryStep.Mary.Length - 1 < conversationLineIndex) return null;
			returnString = currentStoryStep.Mary [conversationLineIndex];
			break;
		case "Sheriff":
			if (currentStoryStep.Sheriff == null || currentStoryStep.Sheriff.Length - 1 < conversationLineIndex) return null;
			returnString = currentStoryStep.Sheriff [conversationLineIndex];
			break;
		case "GunSmith":
			if (currentStoryStep.GunSmith == null || currentStoryStep.GunSmith.Length - 1 < conversationLineIndex) return null;
			returnString = currentStoryStep.GunSmith [conversationLineIndex];
			break;
		case "Daisy":
			if (currentStoryStep.Daisy == null || currentStoryStep.Daisy.Length - 1 < conversationLineIndex) return null;
			returnString = currentStoryStep.Daisy [conversationLineIndex];
			break;
		case "Joe":
			if (currentStoryStep.Joe == null || currentStoryStep.Joe.Length - 1 < conversationLineIndex) return null;
			returnString = currentStoryStep.Joe [conversationLineIndex];
			break;
		}

		return returnString;
	}
}

[System.Serializable]
class StorySteps {
	public List<StoryStep> stroySteps;
}

[System.Serializable]
class StoryStep {

	public bool SceneLock;

	public string CurrentScene;
	public string NextScene;

	public string NextStoryTrigger;
	public string PrevStoryTrigger;

	public string [] Letter;
	public string [] Mary;
	public string [] Sheriff;
	public string [] GunSmith;
	public string [] Daisy;
	public string [] Joe;

}
