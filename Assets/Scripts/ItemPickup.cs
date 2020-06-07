using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

	public int Ammo = 0;
	public int Money = 0;

	public AudioSource SoundFX;

	private bool _willDestroy = false;

	private void OnTriggerEnter (Collider other)
	{
		if (!_willDestroy) {
			_willDestroy = true;
			PlayerInventory.AddAmmo (Ammo);
			PlayerInventory.Money += Money;

			if (SoundFX) SoundFX.Play ();

			GameObject spriteGameObject = transform.GetChild (0).gameObject;
			spriteGameObject.GetComponent<SpriteRenderer> ().enabled = false;

			Destroy (gameObject, 0.3f);
		}

	}

}
