using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	public static int Ammo = 99;
	public static int Money = 0;
	public static int Score = 0;

	public static void AddAmmo (int ammo)
	{
		if (Ammo + ammo > 99) Ammo = 99;
		else Ammo += ammo;
	}

	public static void SubstractAmmo (int ammo)
	{
		if (Ammo + ammo < 0) Ammo = 0;
		else Ammo -= ammo;
	}

	// Update is called once per frame
	void Update ()
	{
		//Debug.Log ("Ammo: " + Ammo);
		//Debug.Log ("Money: " + Money);
		//Debug.Log ("Score: " + Score);
	}
}
