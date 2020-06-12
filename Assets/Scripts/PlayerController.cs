using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	static public bool interactionEnabled = false;

	public Transform playerTransform;
	public CharacterController characterController;
	public GameObject infoText;

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

			if (Shot.transform.tag == "Enemy") {
				//focusedEnemy = Shot.transform;
				// nothing here WeaponFire will take care
			} else if (Shot.transform.tag == "NPC" && targetDistance < maxInteractionDistance) {
				string NPCName = Shot.collider.gameObject.GetComponent<NPC> ().characterName;

				infoText.GetComponent<Text> ().color = Color.black;
				infoText.GetComponent<Text> ().text = "Press button talk to " + NPCName;
				infoText.SetActive (true);
				focusedNPC = Shot.transform;
				interactionEnabled = true;

			} else if (Shot.transform.tag == "Door" && targetDistance < maxInteractionDistance) {
				infoText.GetComponent<Text> ().color = Color.white;
				infoText.GetComponent<Text> ().text = "Press button to Open Door";
				infoText.SetActive (true);
				focusedDoor = Shot.transform;
				interactionEnabled = true;

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
				// TODO: start npc dialog with focusedNPC
				Debug.Log ("- talk to ( how will i get the name ) ?? ");
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
