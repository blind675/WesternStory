using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour {

	public GameObject storyCanvas;
	public GameObject mainCanvas;

	public Image DialogImage;
	public Text DialogLine;

	public Sprite PlayerImage;

	public GameObject inputManager;

	public static int storyStepIndex = 0;

	private int conversationLineIndex = 0;
	private NPC NPC;

	private static string storyJSONString = "{\"stroySteps\":[{\"SceneLock\":true,\"CurrentScene\":\"Town\",\"NextStoryTrigger\":\"Letter\",\"Letter\":[\"PLAYER- My mother went away but she left a letter, I should read it.\",\"Dear Bob, I`ve gone to your grandma for two months. I`ll be back in time for your wedding. Please visit Mary, your fiance, she has something to ask of you.\"]},{\"SceneLock\":true,\"CurrentScene\":\"Town\",\"NextStoryTrigger\":\"Mary\",\"Mary\":[\"My lovely husband to be, I`m so glad you came. I lost Daisy, my cow ever since I was little! We cannot get married without her at the ceremony.\",\"Can you find her for me? She usually hangs around the bear cave outside town. I really hope she`s ok.\",\"PLAYER- Yes mam, I`ll gladly bring Betsy back.\"],\"Sheriff\":[\"Hi Bob. Nice day ain`t it? I hard Mary is looking for ya. You should see what that`s about.\"],\"GunSmith\":[\"Howdy. Want a new gun. We have all you need for the right price.\"]},{\"SceneLock\":false,\"CurrentScene\":\"Town\",\"NextScene\":\"BearCave\",\"NextStoryTrigger\":\"Scene\",\"Mary\":[\"Please find my cow Daisy, we cannot get married without her. Search outside town.\"],\"Sheriff\":[\"You should find that damn cow if you ever get married. Maybe, also, get a new gun.\"],\"GunSmith\":[\"Howdy. Want a new gun. We have all you need for the right price.\"]},{\"SceneLock\":false,\"CurrentScene\":\"BearCave\",\"NextScene\":\"Town\",\"NextStoryTrigger\":\"Daisy\",\"PrevStoryTrigger\":\"Scene\",\"Daisy\":[\"PLAYER- Daisy, here you are! Let`s get home so I can finally marry Mary.\",\"Moooo!\"]},{\"SceneLock\":false,\"CurrentScene\":\"BearCave\",\"NextScene\":\"Town\",\"NextStoryTrigger\":\"Scene\"}]}";

	private static StorySteps stroy;

	private void Start ()
	{
		stroy = JsonUtility.FromJson<StorySteps> (storyJSONString);
	}

	public bool CanTalkToNPC (string npcName)
	{
		string nextLine = GetLineOfConversation (npcName);
		Debug.Log ("- next dialog Line : " + nextLine);

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
		return stroy.stroySteps [storyStepIndex].SceneLock;
	}

	public void Click ()
	{
		SetNextDialogLine ();
	}

	public void talkToNPC (NPC npc)
	{
		NPC = npc;
		conversationLineIndex = 0;

		mainCanvas.SetActive (false);
		storyCanvas.SetActive (true);

		//Debug.Log ("- talk to " + NPC.NPCName);

		SetNextDialogLine ();
	}

	private void SetNextDialogLine ()
	{
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
			}

		} else {
			// else

			// set the correct the correct dialog image
			// set the correct dialog line

			if (nextDialogLine.Contains ("PLAYER-")) {
				string correctString = nextDialogLine.Substring (8);

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
