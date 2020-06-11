using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float activateDistance = 15f;
	public float attackDistance = 10f;
	public int attackDamage = 10;

	public int health = 20;
	public float moveSpeed = 0.02f;

	public AudioSource enemyHitFX;
	public AudioSource enemyAttackFX;

	public GameObject deathPrefab;

	private bool isDying = false;
	private bool enemyOnCoolDown = false;

	// TODO: debug variables
	public float targetDistance;

	void EnemyHit (int damageAmount)
	{
		health -= damageAmount;
		if (enemyHitFX) enemyHitFX.PlayDelayed (0.6f);
	}

	private void Update ()
	{
		if (!isDying) {

			Animator enemyAnimator = transform.GetChild (0).gameObject.GetComponent<Animator> ();

			if (health < 1) {
				// dying animation
				enemyAnimator.SetBool ("isWalking", false);
				enemyAnimator.SetBool ("isAttacking", false);
				enemyAnimator.SetBool ("isDead", true);
				enemyAnimator.SetBool ("isIdle", false);

				isDying = true;

				StartCoroutine (EnemyDyingRoutine ());
			} else {
				RaycastHit Shot;
				if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out Shot)) {

					if (Shot.transform.tag == "Player") {

						targetDistance = Shot.distance;

						if (targetDistance > activateDistance) {
							enemyAnimator.SetBool ("isWalking", false);
							enemyAnimator.SetBool ("isAttacking", false);
							enemyAnimator.SetBool ("isDead", false);
							enemyAnimator.SetBool ("isIdle", false);
						} else {
							if (targetDistance > attackDistance) {
								transform.position = Vector3.MoveTowards (transform.position, Camera.main.transform.position, moveSpeed);
								enemyAnimator.SetBool ("isWalking", true);
								enemyAnimator.SetBool ("isAttacking", false);
							} else {
								// 
								// attack animation
								enemyAnimator.SetBool ("isAttacking", true);
								enemyAnimator.SetBool ("isWalking", false);

								if (!enemyOnCoolDown) {
									// TODO: player hit indicator
									// player hit sound
									if (enemyAttackFX) enemyAttackFX.Play ();
									// player life points decrease
									PlayerHealth.SubstractHealth (attackDamage);

									enemyOnCoolDown = true;
									StartCoroutine (EnemyCoolDownRoutine ());
								}

							}
						}
					}

				}
			}

		}
	}

	IEnumerator EnemyDyingRoutine ()
	{
		yield return new WaitForSeconds (2);

		Vector3 deathPos = this.gameObject.transform.position;
		deathPos.y = 0.25f;

		if (deathPrefab) {
			// spawn droped item 
			GameObject prefabToSpawn = Instantiate (deathPrefab, deathPos, Quaternion.identity);
		}

		// TODO: increase score maybe ?
		Destroy (gameObject);

	}

	IEnumerator EnemyCoolDownRoutine ()
	{
		yield return new WaitForSeconds (1f);

		enemyOnCoolDown = false;
	}

}