using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	static public bool interactionEnabled = false;

	public Transform playerTransform;
	public CharacterController characterController;
	public GameObject infoText;
	public StoryController storyController;

	//public WeaponFire knifeUIObject;
	//public WeaponFire gunUIObject;
	//public WeaponFire doubleGunUIObject;

	// Player settings
	public float moveSpeed;

	public float maxInteractionDistance;

	// TODO: debug variables
	public float targetDistance;

	private Transform focusedDoor;
	private Transform focusedNPC;
	private Transform focusedEnemy;

	// Update is called once per frame
	void Update ()
	{
		// raycast first, party after
		RaycastHit Shot;
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out Shot)) {
			targetDistance = Shot.distance;

			if (Shot.transform.tag == "Weapon") {
				int weaponCost = Shot.collider.gameObject.GetComponent<Weapon> ().cost;

				infoText.SetActive (true);

				if (weaponCost <= PlayerInventory.Money) {
					infoText.GetComponent<Text> ().color = Color.gray;
					infoText.GetComponent<Text> ().text = "Walk Over To Buy Weapon";
				} else {
					infoText.GetComponent<Text> ().color = Color.red;
					infoText.GetComponent<Text> ().text = "You don't have enough Money";
				}

				//focusedEnemy = Shot.transform;
				// nothing here WeaponFire will take care
			} else if (Shot.transform.tag == "NPC" && targetDistance < maxInteractionDistance) {
				NPC npc = Shot.collider.gameObject.GetComponent<NPC> ();

				Debug.Log ("NPC: " + npc.NPCName);
				Debug.Log ("Story index: " + StoryController.storyStepIndex);

				if (storyController.CanTalkToNPC (npc.NPCName)) {
					string NPCInfoText = npc.interractionInfoText;
					infoText.GetComponent<Text> ().color = Color.gray;
					infoText.GetComponent<Text> ().text = NPCInfoText;
					infoText.SetActive (true);
					focusedNPC = Shot.transform;
					interactionEnabled = true;
				}

			} else if (Shot.transform.tag == "Door" && targetDistance < maxInteractionDistance) {
				infoText.SetActive (true);

				if (storyController.CanOpenDoor ()) {
					infoText.GetComponent<Text> ().color = Color.white;
					infoText.GetComponent<Text> ().text = "Press button to Open Door";
					focusedDoor = Shot.transform;
					interactionEnabled = true;
				} else {
					infoText.GetComponent<Text> ().color = Color.red;
					infoText.GetComponent<Text> ().text = "Read Letter First";
				}

			} else {
				infoText.SetActive (false);
				focusedDoor = null;
				focusedNPC = null;
				focusedEnemy = null;
				interactionEnabled = false;
			}
		}

		// player movement 
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

		characterController.SimpleMove (playerTransform.up * -1);

		// player attack and interactions
		if (InputManager.fire || Input.GetKey (KeyCode.LeftAlt)) {
			if (focusedDoor) {
				// open door animation on focusedDoor game object
				focusedDoor.SendMessage ("OpenDoor", SendMessageOptions.DontRequireReceiver);
			} else if (focusedNPC) {
				// start npc dialog with focusedNPC
				InputManager.fire = false;
				NPC selectedNPC = Shot.collider.gameObject.GetComponent<NPC> ();
				storyController.talkToNPC (selectedNPC);
			} else {
				// FIXME: nothing here WeaponFire will take care

				// determine the active weapon
				//WeaponFire activeWeapon = null; ;

				//if (WeaponsController.isKnifeEquiped) {
				//	activeWeapon = knifeUIObject;
				//} else if (WeaponsController.isGunEquiped) {
				//	activeWeapon = gunUIObject;
				//} else if (WeaponsController.isDoubleGunEquiped) {
				//	activeWeapon = doubleGunUIObject;
				//}

				//if (activeWeapon != null) {
				//	// TODO: fire selected weapon even if user hits nothing
				//}


				//if (focusedEnemy) {

				//	Debug.Log ("- hitting enemy");

				//	// TODO: get weapon damage and attack distance from the active weapon
				//	//if (targetDistance < maxAttackDistance) {
				//	//	focusedEnemy.SendMessage ("EnemyHit", attackDamage, SendMessageOptions.DontRequireReceiver);
				//	//	// TODO: blood splash ??
				//	//}
				//}
			}


		}

	}
}
