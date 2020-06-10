using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

	public GameObject KnifeUI;
	public GameObject GunUI;
	public GameObject DoubleGunUI;

	public GameObject KnifeUIHUD;
	public GameObject GunUIHUD;
	public GameObject DoubleGunUIHUD;

	public bool PickingUpKnife = false;
	public bool PickingUpGun = false;
	public bool PickingUpDoubleGun = false;

	public AudioSource SoundFX;

	private bool _willDestroy = false;

	private void OnTriggerEnter (Collider collider)
	{
		if (collider.transform.tag == "Player") {
			if (!_willDestroy) {
				_willDestroy = true;

				KnifeUI.SetActive (PickingUpKnife);
				GunUI.SetActive (PickingUpGun);
				DoubleGunUI.SetActive (PickingUpDoubleGun);

				KnifeUIHUD.SetActive (PickingUpKnife);
				GunUIHUD.SetActive (PickingUpGun);
				DoubleGunUIHUD.SetActive (PickingUpDoubleGun);

				if (SoundFX) SoundFX.Play ();

				GameObject spriteGameObject = transform.GetChild (0).gameObject;
				spriteGameObject.GetComponent<SpriteRenderer> ().enabled = false;

				Destroy (gameObject, 0.3f);
			}
		}
	}
}
