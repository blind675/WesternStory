using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyJoe : MonoBehaviour {

	public float activateDistance = 15f;
	public float attackDistance = 10f;

	public int health = 20;
	public float moveSpeed = 0.03f;

	public AudioSource enemyHitFX;

	public StoryController storyController;

	public GameObject storyCanvas;
	public GameObject mainCanvas;

	public Image DialogImage;
	public Text DialogLine;

	public Sprite PlayerImage;

	private bool isHit = false;
	private bool freez = false;

	private float targetDistance;

	void EnemyHit (int damageAmount)
	{
		if (WeaponsController.isGunEquiped || WeaponsController.isDoubleGunEquiped) {
			// show story UI with custom text
			StartCoroutine (ShowUseKnifeText ());
		} else {

			// else if hit by knife 
			// lower health
			health -= damageAmount;
			if (enemyHitFX) enemyHitFX.PlayDelayed (0.6f);
		}
	}

	private void Update ()
	{
		Animator enemyAnimator = transform.GetChild (0).gameObject.GetComponent<Animator> ();

		if (freez) {

			enemyAnimator.SetBool ("isWalking", false);
			enemyAnimator.SetBool ("isDead", false);
			enemyAnimator.SetBool ("isIdle", true);

		} else if (!isHit) {

			if (health < 1) {
				// dying animation
				enemyAnimator.SetBool ("isWalking", false);
				enemyAnimator.SetBool ("isDead", true);
				enemyAnimator.SetBool ("isIdle", false);

				isHit = true;
				storyController.TalkToNPC (gameObject.GetComponent<NPC> ());

			} else {
				RaycastHit Shot;
				if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out Shot)) {

					if (Shot.transform.tag == "Player") {

						targetDistance = Shot.distance;

						if (targetDistance > activateDistance) {
							enemyAnimator.SetBool ("isWalking", false);
							enemyAnimator.SetBool ("isDead", false);
							enemyAnimator.SetBool ("isIdle", false);
						} else {
							if (targetDistance > attackDistance) {
								transform.position = Vector3.MoveTowards (transform.position, Camera.main.transform.position, moveSpeed);
								enemyAnimator.SetBool ("isWalking", true);
							} else {
								// Joe doesen't attack 

							}
						}
					}

				}
			}

		}
	}

	IEnumerator ShowUseKnifeText ()
	{
		yield return new WaitForSeconds (0.5f);

		freez = true;

		mainCanvas.SetActive (false);
		storyCanvas.SetActive (true);

		DialogImage.sprite = PlayerImage;
		DialogLine.text = "My guns seem to have no effect on him. I should try the knife.";
	}

	public void Click ()
	{
		freez = false;
	}

}