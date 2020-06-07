using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	public static int Health;

	public static bool AddHealth (int health)
	{
		if (Health == 100) return false;
		if (Health + health > 100) {
			Health = 100;
			return true;
		} else {
			Health += health;
			return true;
		}

	}

	public static void SubstractHealth (int health)
	{
		if (Health - health < 1) {
			// TODO: show Game Over / Restart screen
			Health = 0;
		} else {
			Health -= 0;
		}
	}
}
