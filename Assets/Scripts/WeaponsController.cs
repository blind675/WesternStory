using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour {
	// Will hold a list of weapons that the player has
	// used for weapon switching
	public static bool hasKnife = true;
	public static bool hasGun = true;
	public static bool hasDoubleGun = false;

	public static bool isKnifeEquiped = false;
	public static bool isGunEquiped = true;
	public static bool isDoubleGunEquiped = false;

	public GameObject KnifeUI;
	public GameObject GunUI;
	public GameObject DoubleGunUI;

	public GameObject KnifeUIHUD;
	public GameObject GunUIHUD;
	public GameObject DoubleGunUIHUD;

	public GameObject SwitchWeaponButton;

	private void Start ()
	{
		ShowCurrentlyEquipedWeapon ();
	}

	private void Update ()
	{
		if (hasKnife || hasGun || hasDoubleGun) {
			SwitchWeaponButton.SetActive (true);
		}
	}

	private void ShowCurrentlyEquipedWeapon ()
	{
		KnifeUI.SetActive (isKnifeEquiped);
		GunUI.SetActive (isGunEquiped);
		DoubleGunUI.SetActive (isDoubleGunEquiped);

		KnifeUIHUD.SetActive (isKnifeEquiped);
		GunUIHUD.SetActive (isGunEquiped);
		DoubleGunUIHUD.SetActive (isDoubleGunEquiped);
	}

	public void SwitchToNextWeapon ()
	{
		if (isKnifeEquiped) {
			if (hasGun) {
				isGunEquiped = true;
				isKnifeEquiped = false;
			} else if (hasDoubleGun) {
				isDoubleGunEquiped = true;
				isKnifeEquiped = false;
			}
		} else if (isGunEquiped) {
			if (hasDoubleGun) {
				isDoubleGunEquiped = true;
				isGunEquiped = false;
			} else if (hasKnife) {
				isKnifeEquiped = true;
				isGunEquiped = false;
			}
		} else if (isDoubleGunEquiped) {
			if (hasKnife) {
				isKnifeEquiped = true;
				isDoubleGunEquiped = false;
			} else if (hasGun) {
				isGunEquiped = true;
				isDoubleGunEquiped = false;
			}
		}

		ShowCurrentlyEquipedWeapon ();
	}

}
