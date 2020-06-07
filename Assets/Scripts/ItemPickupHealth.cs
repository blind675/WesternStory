using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupHealth : MonoBehaviour {

	public int Health = 0;

	private bool _willDestroy = false;

	private void OnTriggerEnter (Collider other)
	{
		if (!_willDestroy) {

			if (PlayerHealth.AddHealth (Health)) {
				_willDestroy = true;

				GameObject spriteGameObject = transform.GetChild (0).gameObject;
				spriteGameObject.GetComponent<SpriteRenderer> ().enabled = false;

				Destroy (gameObject, 0.3f);
			}

		}

	}

}
