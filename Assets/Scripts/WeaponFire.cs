using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour {

	public float coolDownTime = 0.1f;
	public string animationName;

	public int ammoConsume = 0;
	public float maxAttackDistance = 5f;
	public int attackDamage = 0;

	public AudioSource weaponEmptyFX;

	public GameObject playerAttack;

	// TODO: temporary solution
	public bool knife = false;

	private bool weaponInCoolDown = false;

	// Update is called once per frame
	void Update ()
	{

		if (!weaponInCoolDown) {
			if (InputManager.fire || Input.GetKey (KeyCode.LeftAlt)) {

				if (PlayerInventory.Ammo == 0 && !knife) {
					// TODO: knife equiped exception fix

					if (weaponEmptyFX) weaponEmptyFX.Play ();

				} else {
					// invoke gun shot/ knife hit (raycast)
					object [] attackData = new object [2];
					attackData [0] = attackDamage;
					attackData [1] = maxAttackDistance;

					playerAttack.SendMessage ("Attack", attackData, SendMessageOptions.RequireReceiver);

					// use bullets
					PlayerInventory.SubstractAmmo (ammoConsume);

					// effects and animations
					AudioSource soundFX = gameObject.GetComponent<AudioSource> ();
					if (soundFX) soundFX.Play ();

					if (animationName != null) {
						gameObject.GetComponent<Animator> ().enabled = true;
						gameObject.GetComponent<Animator> ().Play (animationName, -1, 0f);
					}
				}

				weaponInCoolDown = true;
				StartCoroutine (WaitingCoolDown ());
			}
		}

	}

	IEnumerator WaitingCoolDown ()
	{
		yield return new WaitForSeconds (coolDownTime);

		gameObject.GetComponent<Animator> ().enabled = false;

		weaponInCoolDown = false;
	}
}
