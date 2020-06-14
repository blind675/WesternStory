using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
	public string interractionInfoText;
	public string NPCName;
	public Sprite NPCPicture;

	private void Start ()
	{
		if (!StoryController.IsNPCInScene (this)) {
			Destroy (gameObject);
		}
	}
}
