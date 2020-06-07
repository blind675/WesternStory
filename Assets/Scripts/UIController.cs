using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	public Text LocationText;
	public Text MoneyText;
	public Text HealthText;
	public Text AmmoText;

	// Update is called once per frame
	void Update ()
	{
		LocationText.text = "Town";
		MoneyText.text = "$" + PlayerInventory.Money;
		HealthText.text = "" + PlayerHealth.Health + "%";
		AmmoText.text = "" + PlayerInventory.Ammo;
	}
}
