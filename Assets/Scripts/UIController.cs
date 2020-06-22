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
		LocationText.text = GetLocationFromScene ();
		MoneyText.text = "$" + PlayerInventory.Money;
		HealthText.text = "" + PlayerHealth.Health + "%";
		AmmoText.text = "" + PlayerInventory.Ammo;
	}

	private string GetLocationFromScene ()
	{
		if (StoryController.GetSceneName () == "TownScene") {
			return "Town";
		} else if (StoryController.GetSceneName () == "BanditScene") {
			return "Bandit Camp";
		} else {
			return "Bear Cave";
		}
	}
}
