using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	public static int Health = 100;

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
		if (Health - health < 0) {
			Health = 0;
			// show Game Over / Restart screen

			SceneManager.LoadScene ("GameOver");

		} else {
			Health -= health;
		}

		//Debug.Log ("Health :" + Health);
	}
}
