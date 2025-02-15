﻿using System.Collections;
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
		if (collider.transform.tag == "Player" && transform.GetComponent<Weapon> ().cost <= PlayerInventory.Money) {
			if (!_willDestroy) {
				_willDestroy = true;

				PlayerInventory.Money -= transform.GetComponent<Weapon> ().cost;

				KnifeUI.SetActive (PickingUpKnife);
				GunUI.SetActive (PickingUpGun);
				DoubleGunUI.SetActive (PickingUpDoubleGun);

				KnifeUIHUD.SetActive (PickingUpKnife);
				GunUIHUD.SetActive (PickingUpGun);
				DoubleGunUIHUD.SetActive (PickingUpDoubleGun);

				if (SoundFX) SoundFX.Play ();

				GameObject spriteGameObject = transform.GetChild (0).gameObject;
				spriteGameObject.GetComponent<SpriteRenderer> ().enabled = false;

				if (PickingUpKnife) {
					WeaponsController.hasKnife = true;
				}

				if (PickingUpGun) {
					WeaponsController.hasGun = true;
				}

				if (PickingUpDoubleGun) {
					WeaponsController.hasDoubleGun = true;
				}

				WeaponsController.isKnifeEquiped = PickingUpKnife;
				WeaponsController.isGunEquiped = PickingUpGun;
				WeaponsController.isDoubleGunEquiped = PickingUpDoubleGun;

				Destroy (gameObject, 0.3f);
			}
		}
	}
}
