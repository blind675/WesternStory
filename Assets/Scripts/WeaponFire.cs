using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour {

	public float coolDownTime = 0.1f;
	public string animationName;

	public int ammoConsume = 0;
	public float maxAttackDistance = 5f;
	public int attackDamage = 0;

	private bool weaponInCoolDown = false;

	// Update is called once per frame
	void Update ()
	{
		if (!weaponInCoolDown) {
			if (InputManager.fire || Input.GetKey (KeyCode.LeftAlt)) {
				// TODO: check if have enough bullets or knife equiped
				// TODO: invoke enemy damage ( raycast), bullet chsck and consume, bod splash maybe,  

				weaponInCoolDown = true;
				AudioSource soundFX = gameObject.GetComponent<AudioSource> ();
				if (soundFX) soundFX.Play ();

				if (animationName != null) {
					gameObject.GetComponent<Animator> ().enabled = true;
					gameObject.GetComponent<Animator> ().Play (animationName, -1, 0f);
				}

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
